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
            JavaPath = DetermineJavaExecutablePath(javaPath);
        }

        private static string DetermineJavaExecutablePath(string javaPath)
        {
            if(javaPath != null)
            {
                return javaPath;
            }
            else
            {
                var discoveredPath = JavaExecutableDiscovery.DisoverJavaExecutablePath();

                if(discoveredPath == null)
                {
                    throw new ApplicationException(
                        string.Format(
                            "Unable to find a suitable Java executable to use. Please set JAVA_HOME appriopriately or specify one manually"));
                }

                return discoveredPath;
            }
        }

        /// <summary>
        /// Compile <paramref name="sourceFiles"/> using given <paramref name="compilerFlags" />
        /// </summary>
        /// <param name="sourceFiles"></param>
        /// <param name="compilerFlags"></param>
        /// <param name="outputStream"> </param>
        public CompilationResult Compile(IEnumerable<string> sourceFiles, string compilerFlags = null, Stream outputStream = null)
        {
            using (var process = CreateProcess(sourceFiles, compilerFlags))
            {
                Console.WriteLine("{0} {1}", process.StartInfo.FileName, process.StartInfo.Arguments);

                process.Start();

                if (outputStream != null)
                {
                    while (!process.StandardOutput.EndOfStream)
                    {
                        process.StandardOutput.BaseStream.CopyTo(outputStream);
                    }
                }
                else
                {
                    process.StandardOutput.ReadToEnd();
                }

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

       
    }
}