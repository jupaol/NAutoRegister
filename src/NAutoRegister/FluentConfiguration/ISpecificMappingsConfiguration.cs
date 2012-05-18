// -----------------------------------------------------------------------
// <copyright file="ISpecificMappingsConfiguration.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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

    /// <summary>
    /// The specific mappings configuration contract
    /// </summary>
    public interface ISpecificMappingsConfiguration
    {
        /// <summary>
        /// Gets the mappings configuration.
        /// </summary>
        IMappingsConfiguration WithMappings { get; }

        /// <summary>
        /// Gets an instance of the<see cref="ISpecificMappingsConfigurationForType"/> object
        /// representing the options avalaible to the user to register a new type
        /// </summary>
        ISpecificMappingsConfigurationForType For { get; }
    }
}
