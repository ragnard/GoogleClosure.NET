using System;
using System.Collections.Generic;

namespace GoogleClosure
{
    public class MultipleProvideException : Exception
    {
        public MultipleProvideException(string provide, IAnalyzedSourceFile sourceFile, IAnalyzedSourceFile alreadyProvided)
            : base(string.Format("Namespace '{0}' was provided both by '{1}' and '{2}'", provide, sourceFile.Path, alreadyProvided.Path))
        { }
    }

    public class UndefinedNamespaceException : Exception
    {
        public UndefinedNamespaceException(string ns)
            : base(string.Format("Undefined namespace: '{0}'", ns))
        { }

        public UndefinedNamespaceException(string require, string path)
            : base(string.Format("Undefined namespace '{0}' required in '{1}'", require, path))
        { }
    }

    public class CircularDependencyException : Exception
    {
        public CircularDependencyException(IEnumerable<string> traversalPath)
            : base(string.Format("Circular dependency: {0}", String.Join(" -> ", traversalPath)))
        { }
    }

    /// <summary>
    /// Builds a queryable tree given a set of dependencies.
    /// </summary>
    public class DependencyTree
    {
        private readonly ISet<IAnalyzedSourceFile> _sourceFiles;

        private readonly IDictionary<string, IAnalyzedSourceFile> _provides;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceFiles">set of source files to build dependency tree from</param>
        /// <exception cref="MultipleProvideException"></exception>
        /// <exception cref="UndefinedNamespaceException"></exception>
        public DependencyTree(ISet<IAnalyzedSourceFile> sourceFiles)
        {
            _sourceFiles = sourceFiles;
            _provides = new Dictionary<string, IAnalyzedSourceFile>();

            // Ensure nothing was provided twice.
            foreach (var sourceFile in _sourceFiles)
            {
                foreach (var provide in sourceFile.Provided)
                {
                    if (_provides.ContainsKey(provide))
                    {
                        throw new MultipleProvideException(provide, sourceFile, _provides[provide]);
                    }

                    _provides.Add(provide, sourceFile);
                }
            }

            // Check that all required namespaces are provided.
            foreach (var sourceFile in sourceFiles)
            {
                foreach (var require in sourceFile.Required)
                {
                    if (!_provides.ContainsKey(require))
                    {
                        throw new UndefinedNamespaceException(require, sourceFile.Path);
                    }
                }
            }
        }


        public IList<IAnalyzedSourceFile> GetDependencies(params string[] requiredNamespaces)
        {
            return GetDependencies(new List<string>(requiredNamespaces));
        }

        /// <summary>
        /// Get a list of dependencies for a given list of namespaces, in dependency order.
        /// </summary>
        /// <param name="requiredNamespaces">namespaces to get dependencies for</param>
        /// <returns></returns>
        public IList<IAnalyzedSourceFile> GetDependencies(IEnumerable<string> requiredNamespaces)
        {
            var dependencies = new List<IAnalyzedSourceFile>();

            foreach (var requiredNamespace in requiredNamespaces)
            {
                foreach (var source in ResolveDependencies(requiredNamespace))
                {
                    if (!dependencies.Contains(source))
                    {
                        dependencies.Add(source);
                    }
                }
            }

            return dependencies;
        }

        private IList<IAnalyzedSourceFile> ResolveDependencies(string ns,
            IList<IAnalyzedSourceFile> dependencies = null,
            Stack<string> traversalPath = null)
        {
            if (dependencies == null)
            {
                dependencies = new List<IAnalyzedSourceFile>();
            }

            if (traversalPath == null)
            {
                traversalPath = new Stack<string>();
            }

            IAnalyzedSourceFile sourceFile;

            if (!_provides.TryGetValue(ns, out sourceFile))
            {
                throw new UndefinedNamespaceException(ns);
            }

            // Check for circular dependency
            if (traversalPath.Contains(ns))
            {
                traversalPath.Push(ns);

                throw new CircularDependencyException(traversalPath);
            }

            traversalPath.Push(ns);

            foreach (var require in sourceFile.Required)
            {
                ResolveDependencies(require, dependencies, traversalPath);
            }

            dependencies.Add(sourceFile);

            traversalPath.Pop();

            return dependencies;
        }
    }
}
