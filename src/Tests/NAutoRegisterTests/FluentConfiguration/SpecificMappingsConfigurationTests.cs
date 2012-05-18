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
    public class SpecificMappingsConfigurationTests
    {
        [TestClass]
        public class TheWithMappingsProperty
        {
            [TestMethod]
            public void it_should_return_the_current_shared_mappings_configuration_object()
            {
                AutoRegister.Configure().Including.WithMappings.SpecificMappings.WithMappings.Should().NotBeNull().And.Be(AutoRegister.MappingsConfiguration);
            }
        }
    }
}
