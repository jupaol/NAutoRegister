using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NAutoRegister.FluentConfiguration;
using FluentAssertions;

namespace NAutoRegisterTests.FluentConfiguration
{
    [TestClass]
    public class MappingsConfigurationTests
    {
        [TestClass]
        public class TheSpecificMappingsProperty
        {
            [TestMethod]
            public void it_should_return_the_current_shared_specific_mappings_configuration_object()
            {
                AutoRegister.Configure().Including.WithMappings.SpecificMappings.Should().NotBeNull().And.Be(AutoRegister.SpecificMappingsConfiguration);
            }
        }
    }
}
