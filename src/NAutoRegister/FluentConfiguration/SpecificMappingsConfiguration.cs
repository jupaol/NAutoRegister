// -----------------------------------------------------------------------
// <copyright file="SpecificMappingsConfiguration.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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
    /// The specific mappings configuration options
    /// </summary>
    public class SpecificMappingsConfiguration : ISpecificMappingsConfiguration
    {
        /// <summary>
        /// Gets the mappings configuration.
        /// </summary>
        public IMappingsConfiguration WithMappings
        {
            get { return AutoRegister.MappingsConfiguration; }
        }
    }
}
