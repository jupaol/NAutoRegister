using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using NAutoRegister.FluentConfiguration;
using Moq;
using NAutoRegister;

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

        [TestClass]
        public class TheCurrentContainerProperty
        {
            [TestMethod]
            public void it_should_return_the_current_specified_container()
            {
                var mock = new Mock<IContainer>();
                mock.Setup(x => x.Register(It.IsAny<Type>(), It.IsAny<Type>()));

                var sut = AutoRegister.Configure()
                    .Including
                        .Assembly(this.GetType().Assembly)
                    .WithMappings.SpecificMappings
                    .WithContainer.Container(mock.Object);

                sut.CurrentContainer.Should().NotBeNull().And.Be(AutoRegister.ContainerConfiguration.CurrentContainer);
            }
        }

        [TestClass]
        public class TheRegisterTypesMethod
        {
            [TestMethod]
            public void it_should_register_all_the_implementing_types_resolved_from_the_assemblies_in_the_specified_container()
            {
                var mock = new Mock<IContainer>();
                var list = new List<Tuple<Type, Type>>();

                mock.Setup(x => x.Register(It.IsAny<Type>(), It.IsAny<Type>()))
                    .Callback<Type, Type>((x, y) => list.Add(Tuple.Create<Type, Type>(x, y)));

                AutoRegister.Configure()
                    .Including
                        .Assembly(this.GetType().Assembly)
                    .WithMappings.SpecificMappings
                        .For.Type(
                            typeof(IMyContract1<>),
                            x => x == typeof(MyGenericImplementation1c))
                    .WithContainer.Container(mock.Object)
                    .RegisterTypes();

                list.Should().HaveCount(4).And.OnlyHaveUniqueItems().And.NotContainNulls()
                    .And.Contain(Tuple.Create<Type, Type>(typeof(IMyContract1<int>), typeof(MyGenericImplementation1a)))
                    .And.Contain(Tuple.Create<Type, Type>(typeof(IMyContract1<MyDto>), typeof(MyGenericImplementation1b)))
                    .And.Contain(Tuple.Create<Type, Type>(typeof(IMyContract1<MyDto>), typeof(MyGenericImplementation1d)))
                    .And.Contain(Tuple.Create<Type, Type>(typeof(IMyContract1<MyDto>), typeof(MyGenericImplementation1e)));
            }
        }
    }
}
