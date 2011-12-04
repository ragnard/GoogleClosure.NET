using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace GoogleClosure
{
    public class CompilationMessage
    {
        public string File { get; set; }
        public int LineNumber { get; set; }
        public string Message { get; set; }
    }

    public class CompilationResult
    {
        public bool IsSuccessful { get; set; }
        public IList<CompilationMessage> Errors { get; private set; }
        public IList<CompilationMessage> Warnings { get; private set; }

        public CompilationResult(bool isSuccesful)
        {
            IsSuccessful = isSuccesful;
            Errors = new List<CompilationMessage>();
            Warnings = new List<CompilationMessage>();
        }

        private static readonly Regex CompilerOutputMessage = new Regex(@"(?<file>[^\s]+):(?<linenumber>\d+): (?<type>WARNING|ERROR) - (?<message>.+)$", RegexOptions.Multiline);

        public static CompilationResult CreateFrom(string output, bool isSuccesful = false)
        {
            var result = new CompilationResult(isSuccesful);

            foreach (Match match in CompilerOutputMessage.Matches(output))
            {
                if (match.Success)
                {
                    var type = match.Groups["type"].Value;
                    var file = match.Groups["file"].Value;
                    var lineNumber = Int32.Parse(match.Groups["linenumber"].Value);
                    var message = match.Groups["message"].Value;

                    var compilationMessage = new CompilationMessage
                    {
                        File = file,
                        LineNumber = lineNumber,
                        Message = message
                    };

                    switch (type)
                    {
                        case "WARNING":
                            result.Warnings.Add(compilationMessage);
                            break;
                        case "ERROR":
                            result.Errors.Add(compilationMessage);
                            break;
                    }
                }
            }

            return result;
        }
    }
}