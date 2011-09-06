using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GoogleClosure
{
    public static class Helpers
    {
        /// <summary>
        /// Enumerate all javascript files (files with extensions <code>js</code>) given 
        /// a path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static IEnumerable<string> EnumerateJavaScriptFiles(string path)
        {
            return Directory.EnumerateFiles(path, "*.js", SearchOption.AllDirectories);
        }

        /// <summary>
        /// Given a set of analyzed source files, find <code>base.js</code>
        /// </summary>
        /// <param name="sources"></param>
        /// <returns></returns>
        public static IAnalyzedSourceFile GetClosureBaseFile(ISet<IAnalyzedSourceFile> sources)
        {
            return sources.Single(source =>
                                  source.Path.EndsWith("base.js") &&
                                  source.Provided.Count == 1 && source.Provided.Contains("goog"));
        }
    }
}