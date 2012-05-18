using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Jupaol.Testing.AutoFixture;
using NAutoRegister;
using Moq;
using Ploeh.AutoFixture;
using Jupaol.Reflection;
using System.Reflection;

namespace NAutoRegisterTests
{
    [TestClass]
    public class BindingContractTests
    {
        [TestMethod]
        public void it_should_throw_an_exception_when_creating_a_new_instance_with_a_null_type()
        {
            Action invoking = () => new BindingContract(null);

            invoking.ShouldThrow<ArgumentNullException>();
        }

        [TestMethod]
        public void it_can_create_a_new_instance_with_a_valid_type()
        {
            var sut = new BindingContract(this.GetType()).Should().NotBeNull();
        }

        [TestClass]
        public class TheAddBindingConditionMethod
        {
            [TestMethod]
            public void it_should_throw_an_exception_when_the_delegate_provided_is_null()
            {
                using (var h = new BindingContractAutoFixtureHelper())
                {
                    var sut = h.GetSut();
                    sut.Invoking(x => x.AddBindingCondition(null)).ShouldThrow<ArgumentNullException>();
                }
            }

            [TestMethod]
            public void it_should_add_a_new_condition_associated_with_the_specified_delegate()
            {
                using (var h = new BindingContractAutoFixtureHelper())
                {
                    var sut = h.GetSut();
                    Func<Type, bool> condition = x => x.Namespace == this.GetType().Namespace;
                    var res = sut.AddBindingCondition(condition);
                    
                    res.Should().NotBeNull();
                    res.BindingContract.Should().NotBeNull().And.Be(sut);
                    res.EvaluateCondition.Should().Be(condition);
                    sut.BindingConditions.Should().NotBeNull().And.NotBeEmpty().And.NotContainNulls()
                        .And.ContainItemsAssignableTo<BindingCondition>()
                        .And.Contain(res).And.HaveCount(1);
                }
            }
        }

        [TestClass]
        public class TheFilterTypesMethod
        {
            [TestMethod]
            public void it_should_throw_an_exception_when_the_implementing_types_collection_is_null()
            {
                using (var h = new BindingContractAutoFixtureHelper())
                {
                    var sut = h.GetSut();

                    sut.Invoking(x => x.InvokeNonPublicMethod("FilterTypes", null))
                        .ShouldThrow<ArgumentNullException>();
                }
            }

            [TestMethod]
            public void it_should_return_all_the_implementing_types_specified_when_there_are_not_previous_conditions_registered()
            {
                using (var h = new BindingContractAutoFixtureHelper())
                {
                    var sut = h.GetSut();
                    var res = sut.InvokeNonPublicMethod<IEnumerable<Type>>("FilterTypes", h.GetDefaultTypes());

                    res.Should().NotBeNull().And.HaveCount(4).And.BeEquivalentTo(h.GetDefaultTypes());
                }
            }

            [TestMethod]
            public void it_should_filter_the_implementing_types_specified_with_the_binding_conditions_registered()
            {
                using (var h = new BindingContractAutoFixtureHelper())
                {
                    var sut = h.GetSut();
                    sut.AddBindingCondition(x => x == typeof(MyImplementation1));
                    sut.AddBindingCondition(x => x == typeof(MyImplementation1b));
                    var res = sut.InvokeNonPublicMethod<IEnumerable<Type>>("FilterTypes", h.GetDefaultTypes());

                    res.Should().NotBeNull().And.HaveCount(2).And.OnlyHaveUniqueItems()
                        .And.Contain(typeof(MyImplementation1a)).And.Contain(typeof(MyImplementation1c));
                }
            }
        }

        [TestClass]
        public class TheResolveImplementingTypesMethod
        {
            [TestMethod]
            public void it_should_return_the_implementing_types_contained_in_the_given_assemblies()
            {
                using (var h = new BindingContractAutoFixtureHelper())
                {
                    var sut = h.GetSut();
                    sut.InvokeNonPublicMethod<IEnumerable<Type>>("ResolveImplementingTypes", h.GetWholeTypesFromAssemblyCollection())
                        .Should().NotBeNull().And.HaveCount(4)
                        .And.OnlyHaveUniqueItems()
                        .And.Contain(typeof(MyImplementation1))
                        .And.Contain(typeof(MyImplementation1a))
                        .And.Contain(typeof(MyImplementation1b))
                        .And.Contain(typeof(MyImplementation1c));

                    h.WithType(typeof(IMyContract2));
                    sut = h.GetSut();
                    sut.InvokeNonPublicMethod<IEnumerable<Type>>("ResolveImplementingTypes", h.GetWholeTypesFromAssemblyCollection())
                        .Should().NotBeNull().And.HaveCount(1)
                        .And.OnlyHaveUniqueItems()
                        .And.Contain(typeof(MyImplementation2));

                    h.WithType(typeof(IMyContract3));
                    sut = h.GetSut();
                    sut.InvokeNonPublicMethod<IEnumerable<Type>>("ResolveImplementingTypes", h.GetWholeTypesFromAssemblyCollection())
                        .Should().NotBeNull().And.HaveCount(0);

                    h.WithType(typeof(IMyContract2<>));
                    sut = h.GetSut();
                    sut.InvokeNonPublicMethod<IEnumerable<Type>>("ResolveImplementingTypes", h.GetWholeTypesFromAssemblyCollection())
                        .Should().NotBeNull().And.HaveCount(3).And.OnlyHaveUniqueItems()
                        .And.Contain(typeof(MyGenericImplementation2a))
                        .And.Contain(typeof(MyGenericImplementation2b))
                        .And.Contain(typeof(MyGenericImplementation2c));

                    h.WithType(typeof(IMyContract1<MyDto>));
                    sut = h.GetSut();
                    sut.InvokeNonPublicMethod<IEnumerable<Type>>("ResolveImplementingTypes", h.GetWholeTypesFromAssemblyCollection())
                        .Should().NotBeNull().And.HaveCount(3).And.OnlyHaveUniqueItems()
                        .And.Contain(typeof(MyGenericImplementation1b))
                        .And.Contain(typeof(MyGenericImplementation1d))
                        .And.Contain(typeof(MyGenericImplementation1e));

                    h.WithType(typeof(IMyContract1<int>));
                    sut = h.GetSut();
                    sut.InvokeNonPublicMethod<IEnumerable<Type>>("ResolveImplementingTypes", h.GetWholeTypesFromAssemblyCollection())
                        .Should().NotBeNull().And.HaveCount(1).And.OnlyHaveUniqueItems()
                        .And.Contain(typeof(MyGenericImplementation1a));

                    h.WithType(typeof(IMyContract3<>));
                    sut = h.GetSut();
                    sut.InvokeNonPublicMethod<IEnumerable<Type>>("ResolveImplementingTypes", h.GetWholeTypesFromAssemblyCollection())
                        .Should().NotBeNull().And.HaveCount(0);
                }
            }
        }

        [TestClass]
        public class TheProcessImplementingTypesMethod
        {
            [TestMethod]
            public void it_should_throw_an_exception_when_the_assembly_collection_specified_is_null()
            {
                using (var h = new BindingContractAutoFixtureHelper())
                {
                    var sut = h.GetSut();

                    sut.Invoking(x => x.GetImplementingTypes(null)).ShouldThrow<ArgumentNullException>();
                }
            }

            [TestMethod]
            public void it_should_return_an_empty_enumeration_when_the_assembly_collection_is_empty()
            {
                using (var h = new BindingContractAutoFixtureHelper())
                {
                    var sut = h.GetSut();

                    sut.GetImplementingTypes(new List<Type>().ToArray()).Should().NotBeNull().And.BeEmpty();
                }
            }

            [TestMethod]
            public void it_should_return_the_types_implementing_the_spceified_type_filtering_using_the_registered_conditions()
            {
                using (var h = new BindingContractAutoFixtureHelper())
                {
                    h.WithType(typeof(IMyContract1<>));
                    var sut = h.GetSut();

                    sut.AddBindingCondition(x => x == typeof(MyGenericImplementation1c) || x == typeof(MyGenericImplementation1e));

                    sut.GetImplementingTypes(h.GetWholeTypesFromAssemblyCollection().ToArray()).Should()
                        .NotBeNull().And.HaveCount(3).And.OnlyHaveUniqueItems()
                        .And.Contain(typeof(MyGenericImplementation1a))
                        .And.Contain(typeof(MyGenericImplementation1b))
                        .And.Contain(typeof(MyGenericImplementation1d));
                }
            }
        }
    }

    class BindingContractAutoFixtureHelper : AutoFixtureHelper<BindingContract>
    {
        public BindingContractAutoFixtureHelper()
        {
            this.WithType(typeof(IMyContract1));
        }

        public IEnumerable<Type> GetDefaultTypes()
        {
            return new[] { typeof(MyImplementation1), typeof(MyImplementation1a), typeof(MyImplementation1b), typeof(MyImplementation1c) };
        }

        public IEnumerable<Type> GetWholeTypesFromAssemblyCollection()
        {
            List<Type> types = new List<Type>();

            this.GetDefaultAssemblies().ToList().ForEach(x => types.AddRange(x.GetTypes()));

            return types.AsEnumerable();
        }

        public IEnumerable<Assembly> GetDefaultAssemblies()
        {
            return new[] { this.GetType().Assembly };
        }

        public BindingContractAutoFixtureHelper WithType(Type type)
        {
            this.Fixture.Inject<Type>(type);
            return this;
        }
    }
}
