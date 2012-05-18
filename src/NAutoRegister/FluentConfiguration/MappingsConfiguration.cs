// -----------------------------------------------------------------------
// <copyright file="MappingsConfiguration.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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
    /// The mappings configuration options
    /// </summary>
    public class MappingsConfiguration : IMappingsConfiguration
    {
        /// <summary>
        /// Gets the specific mappings.
        /// </summary>
        public ISpecificMappingsConfiguration SpecificMappings
        {
            get { return AutoRegister.SpecificMappingsConfiguration; }
        }
    }
}
