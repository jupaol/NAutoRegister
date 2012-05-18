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
    public class AutoRegisterTests
    {
        [TestClass]
        public class TheConfigureMethod
        {
            [TestMethod]
            public void initializes_scorrectly_when_first_accessed()
            {
                AutoRegister.Configure().Should().NotBeNull();
                AutoRegister.AssemblyConfiguration.Should().NotBeNull();
                AutoRegister.ExcludingAssembliesConfiguration.Should().NotBeNull();
                AutoRegister.IncludingAssembliesConfiguration.Should().NotBeNull();
            }
        }
    }
}
