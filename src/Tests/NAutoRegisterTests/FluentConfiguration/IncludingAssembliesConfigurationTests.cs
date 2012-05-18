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
    public class IncludingAssembliesConfigurationTests
    {
        [TestMethod]
        public void can_create_a_new_instance()
        {
            var sut = new IncludingAssembliesConfiguration();
            sut.Should().NotBeNull();
        }

        [TestClass]
        public class TheExcludingProperty
        {
            [TestMethod]
            public void it_should_return_the_current_shared_excluding_assemblies_configuration_object()
            {
                AutoRegister.Configure().Including.Excluding.Should().NotBeNull().And.Be(AutoRegister.ExcludingAssembliesConfiguration);
            }
        }

        [TestClass]
        public class TheAssemblyMethod
        {
            [TestMethod]
            public void it_should_add_the_specified_assembly_to_its_internal_collection_and_return_the_current_object()
            {
                var sut = new IncludingAssembliesConfiguration();
                sut.Assembly(this.GetType().Assembly).Should().NotBeNull().And.Be(sut);
                sut.IncludedAssemblies.Should().NotBeNull().And.HaveCount(1).And.Contain(this.GetType().Assembly);
            }
        }

        [TestClass]
        public class TheIncludedAssembliesProperty
        {
            [TestMethod]
            public void it_should_return_all_the_included_assemblies()
            {
                var sut = new IncludingAssembliesConfiguration();
                sut.Assembly(this.GetType().Assembly).Should().NotBeNull().And.Be(sut);
                sut.IncludedAssemblies.Should().NotBeNull().And.HaveCount(1).And.Contain(this.GetType().Assembly);
            }
        }

        [TestClass]
        public class TheAssembliesMethod
        {
            [TestMethod]
            public void it_should_add_all_the_specifed_assemblies_to_its_internal_collection_and_return_the_current_object()
            {
                var sut = new IncludingAssembliesConfiguration();
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                sut.Assemblies(assemblies).Should().NotBeNull().And.Be(sut);
                sut.IncludedAssemblies.Should().NotBeNull().And.HaveSameCount(assemblies).And.BeEquivalentTo(assemblies);
            }
        }

        [TestClass]
        public class TheAssemblyFromTypeMethod_WithGenericParameters_T
        {
            [TestMethod]
            public void it_should_add_the_assembly_of_the_specified_generic_type_to_its_internal_collection_and_return_the_current_object()
            {
                var sut = new IncludingAssembliesConfiguration();
                sut.AssemblyFromType<IncludingAssembliesConfigurationTests>().Should().NotBeNull().And.Be(sut);
                sut.IncludedAssemblies.Should().NotBeNull().And.HaveCount(1).And.Contain(this.GetType().Assembly);
            }
        }

        [TestClass]
        public class TheAssemblyFromTypeMethod_WithParameters_Type
        {
            [TestMethod]
            public void it_should_add_the_assembly_of_the_specified_type_parameter_to_its_internal_collection_and_return_the_current_object()
            {
                var sut = new IncludingAssembliesConfiguration();
                sut.AssemblyFromType(this.GetType()).Should().NotBeNull().And.Be(sut);
                sut.IncludedAssemblies.Should().NotBeNull().And.HaveCount(1).And.Contain(this.GetType().Assembly);
            }
        }
    }
}
