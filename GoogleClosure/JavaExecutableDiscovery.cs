using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GoogleClosure
{
    public class JavaExecutableDiscovery
    {
        /// <summary>
        /// Simple, stupid, heuristic to try and find a suitable <code>java.exe</code>
        /// </summary>
        /// <returns></returns>
        public static string DisoverJavaExecutablePath()
        {
            return GetPotentialJavaExecutables().First(File.Exists);
        }

        public static IEnumerable<string> GetPotentialJavaExecutables()
        {
            return from javaHome in GetPotentialJavaHomeDirectories()
                   select Path.Combine(javaHome, "bin", "java.exe");
        }

        public static IEnumerable<string> GetPotentialJavaHomeDirectories()
        {
            return GetJavaHomeFromEnvironmentVariable()
                .Concat(GetJavaHomeFromProgramFilesDirectory());
        }

        public static IEnumerable<string> GetJavaHomeFromEnvironmentVariable()
        {
            var javaHome = Environment.GetEnvironmentVariable("JAVA_HOME", EnvironmentVariableTarget.Process);

            if (javaHome != null)
            {
                yield return javaHome;
            }
        }

        public static IEnumerable<string> GetJavaHomeFromProgramFilesDirectory()
        {
            var jreVersions = new[] { "jre7", "jre6" };

            return from directory in GetProgramFilesDirectories()
                   from jre in jreVersions
                   select Path.Combine(directory, "Java", jre);
        }

        public static IEnumerable<string> GetProgramFilesDirectories()
        {
            yield return Environment.GetEnvironmentVariable("ProgramW6432");
            yield return Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            yield return Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
        }
    }
}
