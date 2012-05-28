using System;
using System.Diagnostics;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace GoogleClosure.MSBuild
{
    /// <summary>
    /// Task for generating a <code>deps</code> file
    /// </summary>
    public class WriteDependencies : Task
    {
        /// <summary>
        /// Path to root of Closure library
        /// </summary>
        [Required]
        public string ClosureBasePath { get; set; }

        /// <summary>
        /// Directories of source files to generate <code>deps</code> file for
        /// </summary>
        [Required]
        public ITaskItem[] Paths { get; set; }

        /// <summary>
        /// Path to generated <code>deps</code> file
        /// </summary>
        [Required]
        public string Output { get; set; }

        public override bool Execute()
        {
            try
            {
                var stopwatch = Stopwatch.StartNew();
                var fileCount = 0;

                using (var writer = new DependencyWriter(Output))
                {
                    foreach (var taskItem in Paths)
                    {
                        Log.LogMessage("Scanning '{0}' for dependencies", taskItem);

                        var scanner = new DirectoryScanner(ClosureBasePath, taskItem.ItemSpec);

                        var dependencies = scanner.GetSourceFiles();

                        fileCount += dependencies.Count;

                        Log.LogMessage("Found '{0}' source files with dependencies", dependencies.Count);

                        writer.WriteDependencies(dependencies);
                    }
                }

                Log.LogMessage("Wrote '{0}' dependencies to '{1}' in '{2}' ms", fileCount, Output, stopwatch.ElapsedMilliseconds);

                return true;
            }
            catch (Exception e)
            {
                Log.LogErrorFromException(e);
                return false;
            }
        }
    }
}