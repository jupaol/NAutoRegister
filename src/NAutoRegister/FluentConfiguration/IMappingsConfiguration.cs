// -----------------------------------------------------------------------
// <copyright file="IMappingsConfiguration.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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
    /// <summary>
    /// The mappings configuration contract
    /// </summary>
    public interface IMappingsConfiguration
    {
        /// <summary>
        /// Gets the specific mappings.
        /// </summary>
        ISpecificMappingsConfiguration SpecificMappings { get; }
    }
}
