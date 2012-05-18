using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using NAutoRegister.FluentConfiguration;

namespace NAutoRegisterTests.FluentConfiguration
{
    [TestClass]
    public class AssemblyConfigurationTests
    {
        [TestClass]
        public class TheIncludingProperty
        {
            [TestMethod]
            public void should_return_the_current_including_assemblies_configuration_object()
            {
                AutoRegister.Configure().Including.Should().Be(AutoRegister.IncludingAssembliesConfiguration);
            }
        }

        [TestClass]
        public class TheExcludingProperty
        {
            [TestMethod]
            public void should_return_the_current_excluding_assemblies_configuration_object()
            {
                AutoRegister.Configure().Excluding.Should().Be(AutoRegister.ExcludingAssembliesConfiguration);
            }
        }
    }
}
