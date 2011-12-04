using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace GoogleClosure
{
    /// <summary>
    /// Simple utility class for invoking the closure compiler JAR.
    /// </summary>
    public class ClosureCompiler
    {
        public string JarPath { get; set; }
        public string JavaPath { get; set; }

        public Action<string> OnStandardOutputWrite { get; set; }
        public Action<string> OnStandardErrorWrite { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jarPath">Path to <code>closure-compiler.jar</code></param>
        /// <param name="javaPath"></param>
        public ClosureCompiler(string jarPath, string javaPath = null)
        {
            JarPath = jarPath;
            JavaPath = javaPath ?? TryDiscoverJavaRuntimePath();
        }

        /// <summary>
        /// Compile <paramref name="sourceFiles"/> using given <paramref name="compilerFlags" />
        /// </summary>
        /// <param name="sourceFiles"></param>
        /// <param name="compilerFlags"></param>
        public CompilationResult Compile(IEnumerable<string> sourceFiles, string compilerFlags = null)
        {
            using (var process = CreateProcess(sourceFiles, compilerFlags))
            {
                Console.WriteLine("{0} {1}", process.StartInfo.FileName, process.StartInfo.Arguments);

                process.Start();

                if (!process.WaitForExit((int)TimeSpan.FromMinutes(1).TotalMilliseconds))
                {
                    throw new Exception("Timeout");
                }

                var output = process.StandardError.ReadToEnd();
                var isSuccesful = process.ExitCode != 0;
                
                return CompilationResult.CreateFrom(output, isSuccesful);
            }
        }

        private Process CreateProcess(IEnumerable<string> sourceFiles, string compilerFlags)
        {
            return new Process
            {
                StartInfo =
                {
                    FileName = JavaPath,
                    Arguments = BuildArguments(sourceFiles, compilerFlags),
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                }
            };
        }

        void process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (OnStandardErrorWrite != null && !String.IsNullOrWhiteSpace(e.Data))
            {
                OnStandardErrorWrite(e.Data);
            }
        }

        void process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (OnStandardOutputWrite != null && !String.IsNullOrWhiteSpace(e.Data))
            {
                OnStandardOutputWrite(e.Data);
            }
        }

        private string BuildArguments(IEnumerable<string> sourceFiles, string compilerFlags = null)
        {
            var sb = new StringBuilder();

            sb.AppendFormat("-jar \"{0}\"", JarPath);

            sb.Append(" ");
            sb.Append(String.Join(" ", sourceFiles.Select(x => "--js \"" + x + "\"")));

            if (compilerFlags != null)
            {
                sb.Append(" " + compilerFlags);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Simple, stupid, brute-force way to try and find a suitable <code>java.exe</code>
        /// </summary>
        /// <returns></returns>
        private static string TryDiscoverJavaRuntimePath()
        {
            return GetPotentialJavaExecutables().First(File.Exists);
        }

        private static IEnumerable<string> GetPotentialJavaExecutables()
        {
            var jreVersions = new[] { "jre7", "jre6" };

            return from directory in GetProgramFilesDirectories()
                   from jre in jreVersions
                   select Path.Combine(directory, "Java", jre, "bin", "java.exe");
        }

        private static IEnumerable<string> GetProgramFilesDirectories()
        {
            yield return Environment.GetEnvironmentVariable("ProgramW6432");
            yield return Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            yield return Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
        }
    }
}