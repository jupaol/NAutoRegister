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
    public class ExcludingAssembliesConfigurationTests
    {
        [TestMethod]
        public void can_create_a_new_instance()
        {
            var sut = new ExcludingAssembliesConfiguration();
            sut.Should().NotBeNull();
        }

        [TestClass]
        public class TheIncludingProperty
        {
            [TestMethod]
            public void it_should_return_the_current_including_property_object()
            {
                var sut = new ExcludingAssembliesConfiguration();
                sut.Including.Should().NotBeNull().And.Be(AutoRegister.IncludingAssembliesConfiguration);
            }
        }

        [TestClass]
        public class TheAssemblyMethod_WithParameters_string
        {
            [TestMethod]
            public void it_should_return_the_current_excluding_assemblies_object_and_it_should_add_the_assembly_name_specified_to_its_internal_collection()
            {
                var sut = new ExcludingAssembliesConfiguration();
                sut.Assembly(this.GetType().Assembly.FullName).Should().NotBeNull().And.Be(sut);
                sut.ExcludedAssemblyNames.Should().NotBeNull().And.HaveCount(1).And.Contain(this.GetType().Assembly.FullName);
            }
        }

        [TestClass]
        public class TheExcludedAssemblyNamesProperty
        {
            [TestMethod]
            public void it_should_return_all_the_assembly_names_added()
            {
                var sut = new ExcludingAssembliesConfiguration();
                sut.Assembly(this.GetType().Assembly.FullName).Should().NotBeNull().And.Be(sut);
                sut.ExcludedAssemblyNames.Should().NotBeNull().And.HaveCount(1).And.Contain(this.GetType().Assembly.FullName);
            }
        }

        [TestClass]
        public class TheAssemblyMethod_WithParameters_Assembly
        {
            [TestMethod]
            public void it_should_add_the_specified_assembly_to_its_internal_collection_and_return_the_current_object()
            {
                var sut = new ExcludingAssembliesConfiguration();
                sut.Assembly(this.GetType().Assembly).Should().NotBeNull().And.Be(sut);
                sut.ExcludedAssemblies.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(1).And.Contain(this.GetType().Assembly);
            }
        }

        [TestClass]
        public class TheExcludedAssembliesProperty
        {
            [TestMethod]
            public void it_should_return_all_the_excluded_assemblies_registered()
            {
                var sut = new ExcludingAssembliesConfiguration();
                sut.Assembly(this.GetType().Assembly).Should().NotBeNull().And.Be(sut);
                sut.ExcludedAssemblies.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(1).And.Contain(this.GetType().Assembly);
            }
        }

        [TestClass]
        public class TheAssembliesMethod
        {
            [TestMethod]
            public void it_should_add_all_the_specified_assemblies_to_its_internal_collection_and_return_the_current_object()
            {
                var sut = new ExcludingAssembliesConfiguration();
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                sut.Assemblies(assemblies).Should().NotBeNull().And.Be(sut);
                sut.ExcludedAssemblies.Should().NotBeNull().And.BeEquivalentTo(assemblies);
            }
        }

        [TestClass]
        public class TheAssemblyFromTypeMethod_WithGenericParams_T
        {
            [TestMethod]
            public void it_should_add_the_assembly_of_the_generic_parameter_specified_and_return_the_current_object()
            {
                var sut = new ExcludingAssembliesConfiguration();
                sut.AssemblyFromType<ExcludingAssembliesConfigurationTests>().Should().NotBeNull().And.Be(sut);
                sut.ExcludedAssemblies.Should().NotBeNull().And.HaveCount(1).And.Contain(this.GetType().Assembly);
            }
        }

        [TestClass]
        public class TheAssemblyFromTypeMethod_WithParameters_Type
        {
            [TestMethod]
            public void it_should_add_the_assembly_of_the_specified_type_to_its_internal_collection_and_return_the_current_object()
            {
                var sut = new ExcludingAssembliesConfiguration();
                sut.AssemblyFromType(this.GetType()).Should().NotBeNull().And.Be(sut);
                sut.ExcludedAssemblies.Should().NotBeNull().And.HaveCount(1).And.Contain(this.GetType().Assembly);
            }
        }
    }
}
