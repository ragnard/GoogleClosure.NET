using System;
using System.Collections.Generic;
using System.IO;

namespace GoogleClosure
{
    /// <summary>
    /// Scans a directory for source files and analyzes dependencies.
    /// </summary>
    public class DirectoryScanner
    {
        private readonly string _closureBasePath;
        private readonly string _path;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="closureBasePath">root of Closure library</param>
        /// <param name="path">directory to scan</param>
        public DirectoryScanner(string closureBasePath, string path = null)
        {
            if (closureBasePath == null)
            {
                throw new ArgumentNullException("closureBasePath");
            }

            if (closureBasePath.LastIndexOf(Path.DirectorySeparatorChar) != closureBasePath.Length - 1)
            {
                closureBasePath = closureBasePath + Path.DirectorySeparatorChar;
            }

            _closureBasePath = closureBasePath;
            _path = path ?? _closureBasePath;
        }

        /// <summary>
        /// Find source files, analyze their dependencies and return result.
        /// </summary>
        /// <returns></returns>
        public SortedDictionary<string, IAnalyzedSourceFile> GetSourceFiles()
        {
            var dependencies = new SortedDictionary<string, IAnalyzedSourceFile>();
            
            var rootUri = new Uri(_closureBasePath);

            foreach (var file in Helpers.EnumerateJavaScriptFiles(_path))
            {
                var fileUri = new Uri(file);

                var relativeUri = rootUri.MakeRelativeUri(fileUri);

                dependencies.Add(relativeUri.ToString(), new AnalyzedSourceFile(file));
            }

            return dependencies;
        }
    }
}
