// -----------------------------------------------------------------------
// <copyright file="ISpecificMappingsConfigurationForType.cs" company="Juan Pablo Olmos Lara (Jupaol)">
//
// jupaol@hotmail.com
// http://jupaol.blogspot.com/
// 
// Copyright (c) 2012, Juan Pablo Olmos Lara (Jupaol)
// All rights reserved.
// 
// </copyright>
// -----------------------------------------------------------------------

namespace NAutoRegister.FluentConfiguration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// This configuration is used when the user wants to set a specific type to be 
    /// automatically registered
    /// </summary>
    public interface ISpecificMappingsConfigurationForType
    {
        /// <summary>
        /// Gets the binding contracts.
        /// </summary>
        IEnumerable<BindingContract> BindingContracts { get; }

        /// <summary>
        /// Starts the mappings configuration for the specified type
        /// </summary>
        /// <typeparam name="T">The contract type (base type, usually an interface or an abstract class) to configure.</typeparam>
        /// <param name="evaluateCondition">The evaluate condition. This condition will be applied to all the types implementing
        /// or inheriting the <c>T</c> type specified. This condition will determine if the
        /// implementing type being evaluated should be auto registered as an implementation
        /// of the <c>T</c> type specified</param>
        /// <returns>
        /// An instance of the <see cref="ISpecificMappingsConfiguration"/> specific mappings
        /// configuration object
        /// </returns>
        ISpecificMappingsConfiguration Type<T>(params Func<Type, bool>[] evaluateCondition);

        /// <summary>
        /// Starts the mappings configuration for the specified type
        /// </summary>
        /// <param name="type">The contract type (base type, usually an interface or an abstract class) to configure.</param>
        /// <param name="evaluateCondition">The evaluate condition. This condition will be applied to all the types implementing
        /// or inheriting the <c>type</c> type specified. This condition will determine if the
        /// implementing type being evaluated should be auto registered as an implementation
        /// of the <c>type</c> type specified</param>
        /// <returns>
        /// An instance of the <see cref="ISpecificMappingsConfiguration"/> specific mappings
        /// configuration object
        /// </returns>
        ISpecificMappingsConfiguration Type(Type type, params Func<Type, bool>[] evaluateCondition);
    }
}
