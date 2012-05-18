using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NAutoRegister;
using FluentAssertions;
using System.Reflection;

namespace NAutoRegisterTests
{
    [TestClass]
    public class AssemblyMergerTests
    {
        [TestMethod]
        public void can_create_a_new_instance()
        {
            var assemblies = new[] { this.GetType().Assembly };
            var sut = new AssemblyMerger(assemblies, assemblies, new[] { this.GetType().Assembly.FullName });
            sut.Should().NotBeNull();
        }

        [TestClass]
        public class TheMergeAssembliesMethod
        {
            [TestMethod]
            public void it_should_return_the_merged_assemblies_which_basically_means_all_the_included_assemblies_except_the_excluding_assemblies()
            {
                var assemblyToExclude = this.GetType().Assembly;
                var includingAssemblies = AppDomain.CurrentDomain.GetAssemblies();
                var excludingAssemblies = new[] { assemblyToExclude};
                var assemblyNamesToExclude = includingAssemblies.Where(x => x.FullName.StartsWith("System")).Select(x => x.FullName);
                var sut = new AssemblyMerger(includingAssemblies, excludingAssemblies, assemblyNamesToExclude);

                var res = sut.MergeAssemblies();
                res.Should().NotBeNull().And.HaveCount(includingAssemblies.Count() - 1 - assemblyNamesToExclude.Count())
                    .And.OnlyHaveUniqueItems().And.ContainItemsAssignableTo<Assembly>()
                    .And.NotContain(assemblyToExclude);

                var modifiedIncluding = includingAssemblies.ToList();
                modifiedIncluding.Remove(assemblyToExclude);
                modifiedIncluding.RemoveAll(x => assemblyNamesToExclude.Any(c => x.FullName == c));
                res.Should().BeEquivalentTo(modifiedIncluding);

                res.Any(x => assemblyNamesToExclude.Any(c => x.FullName == c)).Should().BeFalse();
            }
        }
    }
}
