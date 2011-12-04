using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GoogleClosure
{
    /// <summary>
    /// Interface for a dependency analyzed source file
    /// </summary>
    public interface IAnalyzedSourceFile
    {
        string Path { get; }

        SortedSet<string> Provided { get; }
        SortedSet<string> Required { get; }
    }


    public abstract class AnalyzedSourceFileBase : IAnalyzedSourceFile
    {
        public string Path { get; set; }

        public SortedSet<string> Provided { get; protected set; }

        public SortedSet<string> Required { get; protected set; }

        protected AnalyzedSourceFileBase()
        {
            Provided = new SortedSet<string>();
            Required = new SortedSet<string>();
        }
    }

    /// <summary>
    /// Analyzes dependencies for a given source file.
    /// 
    /// Scans file for all <code>goog.require</code> and <code>goog.provide</code> 
    /// statements.
    /// </summary>
    public class AnalyzedSourceFile : AnalyzedSourceFileBase
    {
        private const string GOOG_BASE_LINE = "var goog = goog || {}; // Identifies this file as the Closure base.";
        private const string BASE_REGEX_STRING = @"^\s*goog\.{0}\(\s*[\'""](.+)[\'""]\s*\)";

        private static readonly Regex PROVIDE_REGEX = new Regex(string.Format(BASE_REGEX_STRING, "provide"));
        private static readonly Regex REQUIRE_REGEX = new Regex(string.Format(BASE_REGEX_STRING, "require"));

        /// <summary>
        /// Analyze source file 
        /// </summary>
        /// <param name="path"></param>
        public AnalyzedSourceFile(string path)
        {
            Path = path;

            AnalyzeDependencies();
        }

        /// <summary>
        /// Scan source file for dependency statements and collect namespaces.
        /// </summary>
        private void AnalyzeDependencies()
        {
            IList<string> lines = new List<string>();

            using(var stream = File.Open(Path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using(var reader = new StreamReader(stream))
            {
                string line;

                while((line = reader.ReadLine()) != null)
                {
                    if(!string.IsNullOrWhiteSpace(line))
                    {
                        lines.Add(line);
                    }
                }
            }

            foreach (var line in lines)
            {
                if(string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var match = PROVIDE_REGEX.Match(line);

                if (match.Success)
                {
                    Provided.Add(match.Groups[1].Value);
                }

                match = REQUIRE_REGEX.Match(line);

                if (match.Success)
                {
                    Required.Add(match.Groups[1].Value);
                }
            }

            foreach (var line in lines)
            {
                if (line == GOOG_BASE_LINE)
                {
                    if (Provided.Count > 0 || Required.Count > 0)
                    {
                        throw new Exception("Base files should not provide or require namespaces.");
                    }

                    Provided.Add("goog");
                }
            }
        }
    }
}