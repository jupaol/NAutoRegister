// -----------------------------------------------------------------------
// <copyright file="IRegisterTypesConfiguration.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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
    /// The register types configuration contract
    /// </summary>
    public interface IRegisterTypesConfiguration
    {
        /// <summary>
        /// Gets the current container.
        /// </summary>
        IContainer CurrentContainer { get; }

        /// <summary>
        /// Registers the types.
        /// </summary>
        void RegisterTypes();
    }
}
