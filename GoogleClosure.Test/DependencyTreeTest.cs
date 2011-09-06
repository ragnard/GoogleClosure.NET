using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GoogleClosure.Test
{
    public class MockedSourceFile : AnalyzedSourceFileBase
    {
        public MockedSourceFile Requiring(params string[] requires)
        {
            foreach (var require in requires)
            {
                Required.Add(require);
            }

            return this;
        }

        public MockedSourceFile Providing(params string[] provides)
        {
            foreach (var provide in provides)
            {
                Provided.Add(provide);
            }

            return this;
        }
    }

    public class DependencyTreeAssertions
    {
        private static ISet<string> GetProvides(IEnumerable<IAnalyzedSourceFile> sourceFiles)
        {
            return new HashSet<string>(sourceFiles.SelectMany(x => x.Provided));
        }

        public static void AssertValidDependencies(IList<IAnalyzedSourceFile> dependencies)
        {
            foreach (var index in Enumerable.Range(0, dependencies.Count))
            {
                var source = dependencies[index];
                var previousProvides = GetProvides(dependencies.Take(index));

                foreach (var require in source.Required)
                {
                    Assert.True(previousProvides.Contains(require), 
                        string.Format("Namespace '{0}' is not provided before being required by '{1}': [{2}]", 
                            require, source.Path, string.Join(", ", dependencies.Select(x => x.Path))));
                }
            }
        }
    }

    public class DependencyTreeTest
    {
        public MockedSourceFile MockSourceFile(string name)
        {
            return new MockedSourceFile { Path = name };
        }

        public DependencyTree CreateTree(params IAnalyzedSourceFile[] files)
        {
            return new DependencyTree(new HashSet<IAnalyzedSourceFile>(files));
        }

        [Fact]
        public void CanGetDependenciesForSingleNamespace()
        {
            var a = MockSourceFile("A").Providing("A").Requiring("B", "C");
            var b = MockSourceFile("B").Providing("B");
            var c = MockSourceFile("C").Providing("C").Requiring("D");
            var d = MockSourceFile("D").Providing("D").Requiring("E");
            var e = MockSourceFile("E").Providing("E");

            var tree = CreateTree(a, b, c, d, e);

            DependencyTreeAssertions.AssertValidDependencies(tree.GetDependencies("A"));
            DependencyTreeAssertions.AssertValidDependencies(tree.GetDependencies("B"));
            DependencyTreeAssertions.AssertValidDependencies(tree.GetDependencies("C"));
            DependencyTreeAssertions.AssertValidDependencies(tree.GetDependencies("D"));
            DependencyTreeAssertions.AssertValidDependencies(tree.GetDependencies("E"));
        }

        [Fact]
        public void CanGetDependenciesForMultipleNamespaces()
        {
            var a = MockSourceFile("A").Providing("A").Requiring("B");
            var b = MockSourceFile("B").Providing("B").Requiring("C");
            var c = MockSourceFile("C").Providing("C");
            var d = MockSourceFile("D").Providing("D").Requiring("B");

            var tree = CreateTree(a, b, c, d);

            DependencyTreeAssertions.AssertValidDependencies(tree.GetDependencies("D", "A"));
        }

        [Fact]
        public void ThrowsExceptionForCircularDependencies()
        {
            var a = MockSourceFile("A").Providing("A").Requiring("B");
            var b = MockSourceFile("B").Providing("B").Requiring("C");
            var c = MockSourceFile("C").Providing("C").Requiring("A");

            var tree = CreateTree(a, b, c);

            Assert.Throws<CircularDependencyException>(() => tree.GetDependencies("A"));
        }

        [Fact]
        public void ThrowsExceptionWhenRequiringUndefinedNamespace()
        {
            var a = MockSourceFile("A").Providing("A").Requiring("B");
            var b = MockSourceFile("B").Providing("B").Requiring("C");
            var c = MockSourceFile("C").Providing("C").Requiring("D");

            Assert.Throws<UndefinedNamespaceException>(() => CreateTree(a, b, c));
        }

        [Fact]
        public void ThrowsExceptionWhenGettingDependenciesForUndefinedNamespace()
        {
            var a = MockSourceFile("A").Providing("A").Requiring("B");
            var b = MockSourceFile("B").Providing("B");

            var tree = CreateTree(a, b);

            Assert.Throws<UndefinedNamespaceException>(() => tree.GetDependencies("C"));
        }
    }
}