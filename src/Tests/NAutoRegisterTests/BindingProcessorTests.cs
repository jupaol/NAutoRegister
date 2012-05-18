using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NAutoRegister;
using FluentAssertions;
using Jupaol.Testing.AutoFixture;
using System.Reflection;
using Ploeh.AutoFixture;
using Moq;
using Jupaol.Reflection;

namespace NAutoRegisterTests
{
    [TestClass]
    public class BindingProcessorTests
    {
        [TestMethod]
        public void it_should_create_a_new_instance()
        {
            using (var h = new BindingProcessorAutoFixtureHelper())
            {
                var sut = h.GetSut();
                sut.Should().NotBeNull();
            }
        }

        [TestMethod]
        public void it_should_throw_an_exception_when_the_assembly_collection_is_null()
        {
            Action invoking = () => new BindingProcessor(null);

            invoking.ShouldThrow<ArgumentNullException>();
        }

        [TestClass]
        public class TheResolveTypesMethod
        {
            [TestMethod]
            public void it_should_return_an_empty_collection_of_types_when_the_assembly_collection_is_empty()
            {
                using (var h = new BindingProcessorAutoFixtureHelper().WithAssemblies(new List<Assembly>()))
                {
                    var sut = h.GetSut();

                    sut.InvokeNonPublicMethod<IEnumerable<Type>>("ResolveTypes", new List<Assembly>())
                        .Should().NotBeNull().And.BeEmpty();
                }
            }

            [TestMethod]
            public void it_should_return_all_the_types_from_all_the_assemblies_specified()
            {
                using (var h = new BindingProcessorAutoFixtureHelper())
                {
                    var sut = h.GetSut();
                    var types = new List<Type>();
                    var assemblies = AppDomain.CurrentDomain.GetAssemblies().AsEnumerable();
                    assemblies.ToList().ForEach(x => types.AddRange(x.GetTypes()));

                    sut.InvokeNonPublicMethod<IEnumerable<Type>>("ResolveTypes", assemblies)
                        .Should().NotBeNull().And.NotContainNulls().And.NotBeEmpty()
                        .And.HaveSameCount(types).And.OnlyHaveUniqueItems()
                        .And.BeEquivalentTo(types);
                }
            }
        }

        [TestClass]
        public class TheAddBindingContractMethod
        {
            [TestMethod]
            public void it_should_throw_an_exception_if_the_binding_contract_is_null()
            {
                using (var h = new BindingProcessorAutoFixtureHelper())
                {
                    var sut = h.GetSut();
                    sut.Invoking(x => x.AddBindingContract(null)).ShouldThrow<ArgumentNullException>();
                }
            }

            [TestMethod]
            public void it_should_add_a_binding_contract_to_be_processed()
            {
                using (var h = new BindingProcessorAutoFixtureHelper())
                {
                    var sut = h.GetSut();
                    var bc = new BindingContract(this.GetType());
                    var res = sut.AddBindingContract(bc);

                    res.Should().NotBeNull().And.Be(bc);
                }
            }

            [TestMethod]
            public void it_should_throw_an_exception_if_the_binding_contract_was_already_registered()
            {
                using (var h = new BindingProcessorAutoFixtureHelper())
                {
                    var sut = h.GetSut();
                    var bc = new BindingContract(this.GetType());
                    sut.AddBindingContract(bc);
                    sut.Invoking(x => x.AddBindingContract(bc)).ShouldThrow<InvalidOperationException>();
                }
            }
        }

        [TestClass]
        public class TheRegisterTypesMethod
        {
            [TestMethod]
            public void it_should_throw_an_exception_if_the_container_specified_is_null()
            {
                using (var h = new BindingProcessorAutoFixtureHelper())
                {
                    var sut = h.GetSut();
                    sut.Invoking(x => x.RegisterTypes(null)).ShouldThrow<ArgumentNullException>();
                }
            }

            [TestMethod]
            public void it_should_not_throw_if_there_are_not_bidning_contracts_registered()
            {
                using (var h = new BindingProcessorAutoFixtureHelper())
                {
                    var sut = h.GetSut();
                    sut.Invoking(x => x.RegisterTypes(h.Container.Object)).ShouldNotThrow();
                }
            }

            [TestMethod]
            public void it_should_register_the_implementing_types_for_each_binding_contract_type_when_the_binding_contract_type_is_not_generic()
            {
                using (var h = new BindingProcessorAutoFixtureHelper())
                {
                    var sut = h.GetSut();
                    var bc1 = new BindingContract(typeof(IMyContract1));
                    var bc2 = new BindingContract(typeof(IMyContract3));
                    var bc3 = new BindingContract(typeof(IMyContract2));
                    bc1.AddBindingCondition(x => x == typeof(MyImplementation1b));
                    sut.AddBindingContract(bc1);
                    sut.AddBindingContract(bc2);
                    sut.AddBindingContract(bc3);
                    sut.RegisterTypes(h.Container.Object);
                    h.RegisteredTypes.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(4).And.OnlyHaveUniqueItems()
                        .And.Contain(Tuple.Create<Type, Type>(typeof(IMyContract1), typeof(MyImplementation1)))
                        .And.Contain(Tuple.Create<Type, Type>(typeof(IMyContract1), typeof(MyImplementation1a)))
                        .And.Contain(Tuple.Create<Type, Type>(typeof(IMyContract1), typeof(MyImplementation1c)))
                        .And.Contain(Tuple.Create<Type, Type>(typeof(IMyContract2), typeof(MyImplementation2)));
                }
            }

            [TestMethod]
            public void it_should_register_the_implementing_types_for_each_binding_contract_type_when_the_binding_contract_type_is_a_closed_generic()
            {
                using (var h = new BindingProcessorAutoFixtureHelper())
                {
                    var sut = h.GetSut();
                    var bc1 = new BindingContract(typeof(IMyContract1<MyDto>));
                    var bc2 = new BindingContract(typeof(IMyContract2<int>));
                    var bc3 = new BindingContract(typeof(IMyContract3<MyDto>));
                    sut.AddBindingContract(bc1);
                    sut.AddBindingContract(bc2);
                    sut.AddBindingContract(bc3);
                    sut.RegisterTypes(h.Container.Object);
                    h.RegisteredTypes.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(4).And.OnlyHaveUniqueItems()
                        .And.Contain(Tuple.Create<Type, Type>(typeof(IMyContract1<MyDto>), typeof(MyGenericImplementation1b)))
                        .And.Contain(Tuple.Create<Type, Type>(typeof(IMyContract1<MyDto>), typeof(MyGenericImplementation1d)))
                        .And.Contain(Tuple.Create<Type, Type>(typeof(IMyContract1<MyDto>), typeof(MyGenericImplementation1e)))
                        .And.Contain(Tuple.Create<Type, Type>(typeof(IMyContract2<int>), typeof(MyGenericImplementation2b)));
                }
            }

            [TestMethod]
            public void it_should_register_the_implementing_types_for_each_binding_contract_type_when_the_binding_contract_type_is_an_opened_generic()
            {
                using (var h = new BindingProcessorAutoFixtureHelper())
                {
                    var sut = h.GetSut();
                    var bc1 = new BindingContract(typeof(IMyContract1<>));
                    var bc2 = new BindingContract(typeof(IMyContract3<>));
                    sut.AddBindingContract(bc1);
                    sut.AddBindingContract(bc2);
                    sut.RegisterTypes(h.Container.Object);
                    h.RegisteredTypes.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(5).And.OnlyHaveUniqueItems()
                        .And.Contain(Tuple.Create<Type, Type>(typeof(IMyContract1<int>), typeof(MyGenericImplementation1a)))
                        .And.Contain(Tuple.Create<Type, Type>(typeof(IMyContract1<MyDto>), typeof(MyGenericImplementation1b)))
                        .And.Contain(Tuple.Create<Type, Type>(typeof(IMyContract1<string>), typeof(MyGenericImplementation1c)))
                        .And.Contain(Tuple.Create<Type, Type>(typeof(IMyContract1<MyDto>), typeof(MyGenericImplementation1d)))
                        .And.Contain(Tuple.Create<Type, Type>(typeof(IMyContract1<MyDto>), typeof(MyGenericImplementation1e)));
                }
            }

            [TestMethod]
            public void it_should_register_all_the_implementing_types_from_a_complex_types_specification()
            {
                using (var h = new BindingProcessorAutoFixtureHelper())
                {
                    var sut = h.GetSut();
                    
                    var bc1 = new BindingContract(typeof(IMyContract4<,,,,>));
                    bc1.AddBindingCondition(x => x == typeof(MyGenericImplementation4b));
                    bc1.AddBindingCondition(x => x == typeof(MyGenericImplementation4d));
                    bc1.AddBindingCondition(x => x == typeof(MyGenericImplementation4e));
                    sut.AddBindingContract(bc1);

                    var bc2 = new BindingContract(typeof(IMyContract3));
                    bc2.AddBindingCondition(x => x == typeof(MyGenericImplementation4b));
                    bc2.AddBindingCondition(x => x == typeof(MyGenericImplementation4d));
                    sut.AddBindingContract(bc2);

                    var bc3 = new BindingContract(typeof(IMyContract3<>));
                    bc3.AddBindingCondition(x => x == typeof(MyGenericImplementation4b));
                    bc3.AddBindingCondition(x => x == typeof(MyGenericImplementation4d));
                    sut.AddBindingContract(bc3);

                    var bc4 = new BindingContract(typeof(IMyContract1));
                    bc4.AddBindingCondition(x => x == typeof(MyImplementation1a));
                    sut.AddBindingContract(bc4);

                    var bc5 = new BindingContract(typeof(IMyContract2));
                    sut.AddBindingContract(bc5);

                    var bc6 = new BindingContract(typeof(IMyContract2<>));
                    bc6.AddBindingCondition(x => x == typeof(MyGenericImplementation2a));
                    bc6.AddBindingCondition(x => x == typeof(MyGenericImplementation2b));
                    sut.AddBindingContract(bc6);

                    var bc7 = new BindingContract(typeof(IMyContract1<>));
                    bc7.AddBindingCondition(x => x == typeof(MyGenericImplementation1c));
                    bc7.AddBindingCondition(x => x == typeof(MyGenericImplementation2a));
                    bc7.AddBindingCondition(x => x == typeof(MyGenericImplementation2b));
                    sut.AddBindingContract(bc7);

                    sut.RegisterTypes(h.Container.Object);
                    h.RegisteredTypes.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(13).And.OnlyHaveUniqueItems();
                }
            }
        }
    }

    class BindingProcessorAutoFixtureHelper : AutoFixtureHelper<BindingProcessor>
    {
        private IList<Tuple<Type, Type>> registeredTypes;

        public Mock<IContainer> Container { get; private set; }
        public IEnumerable<Tuple<Type, Type>> RegisteredTypes { get { return this.registeredTypes.AsEnumerable(); } }

        public BindingProcessorAutoFixtureHelper()
        {
            this.registeredTypes = new List<Tuple<Type, Type>>();
            this.WithAssemblies(this.GetDefaultAssemblies());
            this.Container = new Mock<IContainer>();
            this.Container.Setup(x => x.Register(It.IsAny<Type>(), It.IsAny<Type>()))
                .Callback<Type, Type>((x, y) => this.registeredTypes.Add(Tuple.Create<Type, Type>(x, y)));
        }

        public IEnumerable<Assembly> GetDefaultAssemblies()
        {
            return new[] { this.GetType().Assembly };
        }

        public BindingProcessorAutoFixtureHelper WithAssemblies(IEnumerable<Assembly> assemblies)
        {
            this.Fixture.Inject(assemblies);
            return this;
        }
    }
}
