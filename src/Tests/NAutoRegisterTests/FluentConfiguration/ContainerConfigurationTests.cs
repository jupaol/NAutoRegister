using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NAutoRegister.FluentConfiguration;
using FluentAssertions;
using Moq;
using NAutoRegister;

namespace NAutoRegisterTests.FluentConfiguration
{
    [TestClass]
    public class ContainerConfigurationTests
    {
        [TestMethod]
        public void can_create_a_new_instance()
        {
            var sut = new ContainerConfiguration();
            sut.Should().NotBeNull();
        }

        [TestClass]
        public class TheAddcontainerMethod
        {
            [TestMethod]
            public void it_should_set_the_specified_container_implementation_as_the_default_container()
            {
                var sut = new ContainerConfiguration();
                Mock<IContainer> container = new Mock<IContainer>();
                sut.Container(container.Object).Should().NotBeNull().And.Be(AutoRegister.RegisterTypesConfiguration);
                sut.CurrentContainer.Should().NotBeNull().And.Be(container.Object);
            }
        }
    }
}
