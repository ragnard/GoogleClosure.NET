using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace GoogleClosure.MSBuild
{
    //public class CompileScripts : Task
    public class CompileScripts : AppDomainIsolatedTask
    {
        [Required]
        public ITaskItem[] Roots { get; set; }

        public ITaskItem[] Inputs { get; set; }

        [Required]
        public ITaskItem[] Namespaces { get; set; }

        [Required]
        public string CompilerJar { get; set; }

        public string CompilerFlags { get; set; }

        public string JavaPath { get; set; }

        public override bool Execute()
        {
            Log.LogMessage("Collecting sources from the following roots:");

            foreach (var taskItem in Roots)
            {
                Log.LogMessage("- {0}", taskItem.ItemSpec);
            }

            var sources = CollectSources();

            Log.LogMessage("Collected '{0}' sources", sources.Count);

            Log.LogMessage("Buildling dependency tree");
            var dependencyTree = new DependencyTree(sources);

            Log.LogMessage("Getting dependencies for the following namespaces:");

            var namespaces = Namespaces.Select(x => x.ItemSpec).ToList();

            foreach (var ns in namespaces)
            {
                Log.LogMessage("- {0}", ns);
            }

            var dependencies = dependencyTree.GetDependencies(namespaces);

            dependencies.Insert(0, Helpers.GetClosureBaseFile(sources));

            Log.LogMessage("Found '{0}' dependencies", dependencies.Count);

            foreach (var dependency in dependencies)
            {
                Log.LogMessage("- {0}", dependency.Path);
            }

            var compiler = new ClosureCompiler(CompilerJar, JavaPath);
            compiler.OnStandardErrorWrite = line => Log.LogWarning(line);
            compiler.OnStandardOutputWrite = line => Log.LogMessage(line);
            
            Log.LogMessage("Executing compiler");
            Log.LogMessage("- Compiler: '{0}'", CompilerJar);

            if (JavaPath != null)
            {
                Log.LogMessage("- Java: '{0}'", JavaPath);
            }

            if(CompilerFlags != null)
            {
                Log.LogMessage("- Compiler flags: '{0}'", CompilerFlags);
            }

            var result = compiler.Compile(dependencies.Select(x => x.Path), CompilerFlags);

            foreach(var error in result.Errors)
            {
                Log.LogError("GoogleClosure Compiler", null, null, error.File, error.LineNumber, 0, 0, 0, error.Message);
            }
            foreach (var warning in result.Warnings)
            {
                Log.LogWarning("GoogleClosure Compiler", null, null, warning.File, warning.LineNumber, 0, 0, 0, warning.Message);
            }

            return true;
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

