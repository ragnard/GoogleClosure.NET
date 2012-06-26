using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace GoogleClosure.MSBuild
{
    public class CompileScripts : AppDomainIsolatedTask
    {
        [Required]
        public ITaskItem[] Roots { get; set; }

        public ITaskItem[] Inputs { get; set; }

        [Required]
        public ITaskItem[] Namespaces { get; set; }

        [Required]
        public string CompilerJar { get; set; }

        public string CompilationLevel { get; set; }

        public string JavaPath { get; set; }

        public string JavaFlags { get; set; }

        [Required]
        public string OutputFile { get; set; }

        public string OutputSourceMapFile { get; set; }

        public string SourceMapVersion { get; set; }

        public string AdditionalCompilerFlags { get; set; }

        public override bool Execute()
        {
            var currentDirectory = Environment.CurrentDirectory;

            try
            {
                var stopwatch = Stopwatch.StartNew();

                var rootDirectory = Path.GetDirectoryName(OutputFile);

                if (rootDirectory == null)
                {
                    throw new ApplicationException("Unable to determine root directory for output file");
                }

                Log.LogMessage("Using '{0}' as root directory for relative paths", rootDirectory);

                Environment.CurrentDirectory = rootDirectory;

                var sourceFiles = GetSourceFiles();

                CompileSourceFiles(rootDirectory, sourceFiles);

                Log.LogMessage("Compilation took '{0}' ms", stopwatch.ElapsedMilliseconds);

                if (OutputSourceMapFile != null)
                {
                    //FixSourceMapUris(OutputSourceMapFile);
                }

                return true;
            }
            catch (Exception e)
            {
                Log.LogErrorFromException(e);
                return false;
            }
            finally
            {
                Environment.CurrentDirectory = currentDirectory;
            }
        }

        private void FixSourceMapUris(string outputSourceMapFile)
        {
            Log.LogMessage("Converting source map paths to URIs");

            var tempSourceMapFile = Path.GetTempFileName();

            using (var stream = new FileStream(outputSourceMapFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            using (var writer = new StreamWriter(tempSourceMapFile, false, Encoding.UTF8))
            {
                while (true)
                {
                    var line = reader.ReadLine();

                    if(line == null)
                    {
                        break;
                    }

                    var modifiedLine = line.Replace("\\\\", "/");
                    writer.Write(modifiedLine);
                }
            }

            File.Replace(tempSourceMapFile, outputSourceMapFile, null);
        }

        private IEnumerable<IAnalyzedSourceFile> GetSourceFiles()
        {
            Log.LogMessage("Collecting source files from the following roots:");

            foreach (var taskItem in Roots)
            {
                Log.LogMessage("- {0}", taskItem.ItemSpec);
            }

            var sources = CollectSources();

            Log.LogMessage("Collected '{0}' sources", sources.Count);

            Log.LogMessage("Buildling dependency tree");
            var dependencyTree = new DependencyTree(sources);

            Log.LogMessage("Determining dependencies for the following namespaces:");

            var namespaces = Namespaces.Select(x => x.ItemSpec).ToList();

            foreach (var ns in namespaces)
            {
                Log.LogMessage("- {0}", ns);
            }

            var dependencies = dependencyTree.GetDependencies(namespaces);

            // base.js goes first
            dependencies.Insert(0, Helpers.GetClosureBaseFile(sources));

            Log.LogMessage("Found '{0}' required source files", dependencies.Count);

            foreach (var dependency in dependencies)
            {
                Log.LogMessage("- {0}", dependency.Path);
            }
            return dependencies;
        }

        private void CompileSourceFiles(string rootDirectory, IEnumerable<IAnalyzedSourceFile> sourceFiles)
        {
            var compiler = new ClosureCompiler(CompilerJar, JavaPath, JavaFlags);
            compiler.OnStandardErrorWrite = line => Log.LogWarning(line);
            compiler.OnStandardOutputWrite = line => Log.LogMessage(line);

            Log.LogMessage("Executing compiler");
            Log.LogMessage("- Compiler: '{0}'", CompilerJar);

            if (JavaPath != null)
            {
                Log.LogMessage("- Java: '{0}'", JavaPath);
            }

            var compilerFlags = BuildCompilerFlags(rootDirectory);

            Log.LogMessage("- Compiler flags: '{0}'", compilerFlags);

            var files = sourceFiles.Select(x => GetPathRelativeTo(rootDirectory, x.Path)).ToList();

            var result = compiler.Compile(files, compilerFlags);

            foreach (var error in result.Errors)
            {
                Log.LogError("GoogleClosure Compiler", null, null, error.File, error.LineNumber, 0, 0, 0, error.Message);
            }
            foreach (var warning in result.Warnings)
            {
                Log.LogWarning("GoogleClosure Compiler", null, null, warning.File, warning.LineNumber, 0, 0, 0, warning.Message);
            }
        }

        private string BuildCompilerFlags(string rootDirectory)
        {
            var flags = new List<string>();

            flags.Add(string.Format(@"--js_output_file=""{0}""", Path.GetFileName(OutputFile)));

            var compilationLevel = GetCompilationLevel();

            if (compilationLevel != null)
            {
                flags.Add(string.Format("--compilation_level={0}", GetCompilationLevel()));
            }

            if (!string.IsNullOrWhiteSpace(OutputSourceMapFile))
            {
                flags.Add(string.Format(@"--create_source_map=""{0}""", GetPathRelativeTo(rootDirectory, OutputSourceMapFile)));
                flags.Add(string.Format("--source_map_format={0}", SourceMapVersion ?? "V3"));
            }

            if (!string.IsNullOrWhiteSpace(AdditionalCompilerFlags))
            {
                flags.Add(AdditionalCompilerFlags);
            }

            return string.Join(" ", flags);
        }

        private string GetCompilationLevel()
        {
            if (CompilationLevel == null)
            {
                return null;
            }

            switch (CompilationLevel.ToUpperInvariant())
            {
                case "ADVANCED":
                    return "ADVANCED_OPTIMIZATIONS";
                case "SIMPLE":
                    return "SIMPLE_OPTIMIZATIONS";
                case "WHITESPACE":
                default:
                    return "WHITESPACE_ONLY";
            }
        }

        private static string GetPathRelativeTo(string dir, string path)
        {
            if (!dir.EndsWith("\\")) dir = dir + "\\";

            var rootUri = new Uri(dir);
            var pathUri = new Uri(path);

            var relativeUri = rootUri.MakeRelativeUri(pathUri);

            return relativeUri.ToString();
        }

        private ISet<IAnalyzedSourceFile> CollectSources()
        {
            if (Roots == null)
            {
                return new HashSet<IAnalyzedSourceFile>();
            }

            return new HashSet<IAnalyzedSourceFile>(
                Roots.SelectMany(root => Helpers.EnumerateJavaScriptFiles(root.ItemSpec)
                                            .Select(file => (IAnalyzedSourceFile)new AnalyzedSourceFile(file))));

        }
    }
}

