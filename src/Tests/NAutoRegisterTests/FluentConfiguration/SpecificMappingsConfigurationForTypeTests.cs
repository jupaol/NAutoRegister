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
    public class SpecificMappingsConfigurationForTypeTests
    {
        [TestMethod]
        public void can_create_a_new_instance()
        {
            var sut = new SpecificMappingsConfigurationForType();
            sut.Should().NotBeNull();
        }

        [TestClass]
        public class TheTypeMethod_WithGenericArguments_T
        {
            [TestMethod]
            public void it_should_create_a_new_binding_contract_with_its_binding_conditions_and_store_it_in_its_internal_collection()
            {
                var sut = new SpecificMappingsConfigurationForType();
                Func<Type, bool> f1 = x => x == typeof(MyGenericImplementation4b);
                Func<Type, bool> f2 = x => x == typeof(MyImplementation1c);
                sut.Type<IMyContract1>(f1, f2);
                sut.BindingContracts.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(1);
                sut.BindingContracts.ToList().ForEach(x =>
                {
                    x.CurrentContractType.Should().Be <IMyContract1>();
                    x.BindingConditions.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(2)
                        .And.OnlyHaveUniqueItems();
                    x.BindingConditions.Any(c => c.EvaluateCondition == f1 || c.EvaluateCondition == f2)
                        .Should().BeTrue();
                });
            }
        }

        [TestClass]
        public class TheTypeMethod_WithArguments_Type
        {
            [TestMethod]
            public void it_should_create_a_new_binding_contract_with_its_binding_conditions_and_store_it_in_its_internal_collection()
            {
                var sut = new SpecificMappingsConfigurationForType();
                Func<Type, bool> f1 = x => x == typeof(MyGenericImplementation4b);
                Func<Type, bool> f2 = x => x == typeof(MyImplementation1c);
                sut.Type(typeof(IMyContract4<,,,,>), f1, f2);
                sut.BindingContracts.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(1);
                sut.BindingContracts.ToList().ForEach(x =>
                {
                    x.CurrentContractType.Should().Be(typeof(IMyContract4<,,,,>));
                    x.BindingConditions.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(2)
                        .And.OnlyHaveUniqueItems();
                    x.BindingConditions.Any(c => c.EvaluateCondition == f1 || c.EvaluateCondition == f2)
                        .Should().BeTrue();
                });
            }

            [TestMethod]
            public void it_should_return_the_current_shared_specific_mappings_configuration_object()
            {
                var sut = new SpecificMappingsConfigurationForType();
                sut.Type(this.GetType()).Should().NotBeNull().And.Be(AutoRegister.SpecificMappingsConfiguration);
            }

            [TestMethod]
            public void it_should_throw_an_exception_when_a_null_delegate_is_provided()
            {
                var sut = new SpecificMappingsConfigurationForType();
                sut.Invoking(x => x.Type(this.GetType(), (Func<Type, bool>)null)).ShouldThrow<ArgumentException>();
            }
        }
    }
}
