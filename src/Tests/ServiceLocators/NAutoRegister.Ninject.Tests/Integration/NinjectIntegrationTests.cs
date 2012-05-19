using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NAutoRegister.FluentConfiguration;
using FluentAssertions;
using NAutoRegister.NinjectProvider;
using Ninject;

namespace NAutoRegister.Ninject.Tests.Integration
{
    [TestClass]
    public class NinjectIntegrationTests
    {
        [TestMethod]
        public void it_should_integrate_Ninject_with_NAutoRegister()
        {
            var kernel = new StandardKernel();
            AutoRegister.Configure()
                .Including.Assembly(this.GetType().Assembly)
                .WithMappings.SpecificMappings.For.Type<IMyContract1<int>>()
                .WithContainer.Container(new NinjectContainerProvider(kernel))
                .RegisterTypes();

            kernel.Get<IMyContract1<int>>().Should().NotBeNull().And.BeAssignableTo<MyContract1>();
        }

        [TestMethod]
        public void it_should_map_open_generic_types()
        {
            var kernel = new StandardKernel();
            AutoRegister.Configure().Including.Assembly(this.GetType().Assembly)
                .WithMappings.SpecificMappings.For.Type(typeof(IMyContract7<>))
                .WithMappings.SpecificMappings.For.Type(typeof(IMyContract2<>),
                    x => x == typeof(MyGenericImplementation2a))
                .WithMappings.SpecificMappings.For.Type(typeof(IMyContract4<,,,,>),
                    x => x == typeof(MyGenericImplementation4b),
                    x => x == typeof(MyGenericImplementation4e))
                .WithContainer.Container(new NinjectContainerProvider(kernel)).RegisterTypes();

            kernel.Get<IMyContract7<int>>().Should().NotBeNull().And.BeAssignableTo<MyImplementation7a>();
            kernel.GetAll<IMyContract2<MyDto>>().Should().HaveCount(1);
            kernel.GetAll<IMyContract4<MyDto3, bool, Int64, MyDto, Tuple<int, Tuple<decimal, string>>>>().Should().HaveCount(3);
            kernel.GetAll<IMyContract4<MyDto, int, string, MyDto, decimal>>().Should().HaveCount(1);
            kernel.GetAll<IMyContract4<MyDto, int, string, MyDto2, decimal>>().Should().HaveCount(0);
        }

        [TestMethod]
        public void it_should_map_closed_generic_types()
        {
            var kernel = new StandardKernel();
            AutoRegister.Configure().Including.Assembly(this.GetType().Assembly)
                .WithMappings.SpecificMappings.For.Type(typeof(IMyContract4<MyDto3, bool, Int64, MyDto, Tuple<int, Tuple<decimal, string>>>),
                    x => x == typeof(MyGenericImplementation4d),
                    x => x == typeof(MyGenericImplementation4d),
                    x => x == typeof(MyGenericImplementation4d),
                    x => x == typeof(MyGenericImplementation4d))
                .WithMappings.SpecificMappings.For.Type(typeof(IMyContract4<MyDto, int, string, MyDto, decimal>),
                    x => x == typeof(MyGenericImplementation4d))
                .WithMappings.SpecificMappings.For.Type(typeof(IMyContract2<MyDto>))
                .WithMappings.SpecificMappings.For.Type(typeof(IMyContract3a))
                .WithMappings.SpecificMappings.For.Type(typeof(IMyContract3b<int>))
                .WithContainer.Container(new NinjectContainerProvider(kernel)).RegisterTypes();

            kernel.GetAll<IMyContract4<MyDto3, bool, Int64, MyDto, Tuple<int, Tuple<decimal, string>>>>()
                .Should().HaveCount(3);
            kernel.GetAll<IMyContract4<MyDto, int, string, MyDto, decimal>>()
                .Should().HaveCount(1);
            kernel.GetAll<IMyContract2<MyDto>>().Should().HaveCount(2);
            kernel.GetAll<IMyContract3a>().Should().HaveCount(0);
            kernel.GetAll<IMyContract3b<int>>().Should().HaveCount(0);
        }

        [TestMethod]
        public void it_should_map_non_generic_interfaces()
        {
            var kernel = new StandardKernel();
            AutoRegister.Configure().Including.Assembly(this.GetType().Assembly)
                .WithMappings.SpecificMappings.For.Type<IMyContract5>(
                    x => x == typeof(MyImplementation5c))
                .WithMappings.SpecificMappings.For.Type<IMyContract6>(
                    x => x == typeof(MyImplementation6c),
                    x => x == typeof(MyImplementation6c),
                    x => x == typeof(MyImplementation6d))
                .WithContainer.Container(new NinjectContainerProvider(kernel)).RegisterTypes();

            kernel.GetAll<IMyContract5>().Should().HaveCount(3);
            kernel.GetAll<IMyContract6>().Should().HaveCount(6);
        }
    }

    class MyDto { }
    class MyDto2 { }
    class MyDto3 { }
    
    interface IMyContract2<T> { }
    interface IMyContract3a { }
    interface IMyContract3b<T> { }
    interface IMyContract4<T, W, X, Y, Z> { }
    interface IMyContract5 { }
    interface IMyContract6 { }
    interface IMyContract7<T> { }

    class MyImplementation7a : IMyContract7<int> { }

    class MyImplementation5a : IMyContract5 { }
    class MyImplementation5b : IMyContract5 { }
    class MyImplementation5c : IMyContract5 { }
    class MyImplementation5d : IMyContract5 { }

    class MyImplementation6a : IMyContract6 { }
    class MyImplementation6b : IMyContract6 { }
    class MyImplementation6c : IMyContract6 { }
    class MyImplementation6d : IMyContract6 { }
    class MyImplementation6e : IMyContract6 { }
    class MyImplementation6f : IMyContract6 { }
    class MyImplementation6g : IMyContract6 { }
    class MyImplementation6h : IMyContract6 { }

    class MyGenericImplementation2a : IMyContract2<MyDto> { }
    class MyGenericImplementation2b : IMyContract2<int> { }
    class MyGenericImplementation2c : IMyContract2<MyDto> { }

    class MyGenericImplementation4a : IMyContract4<MyDto, int, string, MyDto, decimal> { }
    class MyGenericImplementation4b : IMyContract4<float, string, int, MyDto2, float> { }
    class MyGenericImplementation4c : IMyContract4<bool, MyDto2, MyDto, MyDto, int> { }
    class MyGenericImplementation4d : IMyContract4<char, MyDto3, bool, int, List<int>> { }
    class MyGenericImplementation4e : IMyContract4<Int64, float, string, MyDto3, Dictionary<int, Type>> { }
    class MyGenericImplementation4f : IMyContract4<MyDto2, decimal, int, decimal, IEnumerable<float>> { }
    class MyGenericImplementation4g : IMyContract4<MyDto3, bool, Int64, MyDto, Tuple<int, Tuple<decimal, string>>> { }
    class MyGenericImplementation4h : IMyContract4<MyDto3, bool, Int64, MyDto, Tuple<int, Tuple<decimal, string>>> { }
    class MyGenericImplementation4i : IMyContract4<MyDto3, bool, Int64, MyDto, Tuple<int, Tuple<decimal, string>>> { }
}
