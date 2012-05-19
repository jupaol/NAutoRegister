using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NAutoRegister.NinjectProvider;
using FluentAssertions;
using Ninject;

namespace NAutoRegister.Ninject.Tests
{
    [TestClass]
    public class NinjectContainerProviderTests
    {
        [TestMethod]
        public void can_create_a_new_instance_without_parameters()
        {
            var sut = new NinjectContainerProvider();
            sut.Should().NotBeNull();
        }

        [TestMethod]
        public void when_creating_a_new_instance_withaout_null_kernel_it_should_an_exception()
        {
            Action invoking = () => new NinjectContainerProvider(null);
            invoking.ShouldThrow<ArgumentNullException>();
        }

        [TestMethod]
        public void it_should_create_a_new_instance_when_specifying_a_valid_Ninject_kernel()
        {
            var sut = new NinjectContainerProvider(new StandardKernel());
            sut.Should().NotBeNull();
        }

        [TestClass]
        public class TheRegisterMethod_WithParameters_Type_Type
        {
            [TestMethod]
            public void it_should_register_the_types_specified_in_the_Ninject_container()
            {
                var kernel = new StandardKernel();
                var sut = new NinjectContainerProvider(kernel);
                sut.Register(typeof(IMyContract1<int>), typeof(MyContract1));
                kernel.Get<IMyContract1<int>>().Should().NotBeNull().And.BeAssignableTo<MyContract1>();
            }
        }

        [TestClass]
        public class TheRegisterMethod_WithGenericParameters_T_X
        {
            [TestMethod]
            public void it_should_register_the_generic_types_specified_in_the_Ninject_container()
            {
                var kernel = new StandardKernel();
                var sut = new NinjectContainerProvider(kernel);
                sut.Register<IMyContract1<int>, MyContract1>();
                kernel.Get<IMyContract1<int>>().Should().NotBeNull().And.BeAssignableTo<MyContract1>();
            }
        }

        [TestClass]
        public class TheResolveMethod_WithGenericParameters_T
        {
            [TestMethod]
            public void it_should_resolve_the_specifed_generic_type_from_the_Ninject_container()
            {
                var kernel = new StandardKernel();
                var sut = new NinjectContainerProvider(kernel);
                sut.Register<IMyContract1<int>, MyContract1>();
                sut.Resolve<IMyContract1<int>>().Should().NotBeNull().And.BeAssignableTo<MyContract1>();
            }
        }

        [TestClass]
        public class TheResolveMethod_WithParameters_Type
        {
            [TestMethod]
            public void it_should_resolve_the_specifed_type_from_the_Ninject_container()
            {
                var kernel = new StandardKernel();
                var sut = new NinjectContainerProvider(kernel);
                sut.Register(typeof(IMyContract1<int>), typeof(MyContract1));
                sut.Resolve(typeof(IMyContract1<int>)).Should().NotBeNull().And.BeAssignableTo<MyContract1>();
                sut.Resolve(typeof(MyContract1)).Should().NotBeNull()
                    .And.BeAssignableTo<MyContract1>()
                    .And.BeAssignableTo<IMyContract1<int>>();
            }
        }
    }

    interface IMyContract1<T> { }
    class MyContract1 : IMyContract1<int> { }
}
