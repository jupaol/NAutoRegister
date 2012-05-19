// -----------------------------------------------------------------------
// <copyright file="IContainer.cs" company="Juan Pablo Olmos Lara (Jupaol)">
//
// jupaol@hotmail.com
// http://jupaol.blogspot.com/
// 
// Copyright (c) 2012, Juan Pablo Olmos Lara (Jupaol)
// All rights reserved.
// 
// </copyright>
// -----------------------------------------------------------------------

namespace NAutoRegister
{
    using System;

    /// <summary>
    /// Container contract used to automatically register the types
    /// </summary>
    public interface IContainer
    {
        /// <summary>
        /// Resolves the specified type
        /// </summary>
        /// <typeparam name="T">The type to resolve</typeparam>
        /// <returns>
        /// The resolved type
        /// </returns>
        T Resolve<T>() where T : class;

        /// <summary>
        /// Resolves the specified type.
        /// </summary>
        /// <param name="type">The type to resolve.</param>
        /// <returns>
        /// The resolved type
        /// </returns>
        object Resolve(Type type);

        /// <summary>
        /// Registers the implementation type with the contract type specified
        /// </summary>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        void Register<TContract, TImplementation>() where TImplementation : class, TContract;

        /// <summary>
        /// Registers the implementation type with the contract type specified
        /// </summary>
        /// <param name="contractType">Type of the contract.</param>
        /// <param name="implementationType">Type of the implementation.</param>
        void Register(Type contractType, Type implementationType);
    }
}
