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
    public class RegisterTypesConfigurationTests
    {
        [TestMethod]
        public void can_create_a_new_instance()
        {
            var sut = new RegisterTypesConfiguration();
            sut.Should().NotBeNull();
        }
    }
}
