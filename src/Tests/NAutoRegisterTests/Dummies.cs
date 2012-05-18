// -----------------------------------------------------------------------
// <copyright file="Dummies.cs" company="Juan Pablo Olmos Lara (Jupaol)">
//
// jupaol@hotmail.com
// http://jupaol.blogspot.com/
// 
// Copyright (c) 2012, Juan Pablo Olmos Lara (Jupaol)
// All rights reserved.
// 
// </copyright>
// -----------------------------------------------------------------------

namespace NAutoRegisterTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    interface IMyContract1<T> { }
    interface IMyContract2<T> { }
    interface IMyContract3<T> { }
    interface IMyContract4<T, W, X, Y, Z> { }
    interface IMyContract1 { }
    interface IMyContract2 { }
    interface IMyContract3 { }
    class MyDto { }
    class MyDto2 { }
    class MyDto3 { }
    class MyImplementation1 : IMyContract1 { }
    class MyImplementation2 : IMyContract2 { }
    class MyImplementation1a : IMyContract1 { }
    class MyImplementation1b : IMyContract1 { }
    class MyImplementation1c : IMyContract1 { }
    class MyGenericImplementation1a : IMyContract1<int> { }
    class MyGenericImplementation1b : IMyContract1<MyDto> { }
    class MyGenericImplementation1c : IMyContract1<string> { }
    class MyGenericImplementation1d : IMyContract1<MyDto> { }
    class MyGenericImplementation1e : IMyContract1<MyDto> { }
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
}
