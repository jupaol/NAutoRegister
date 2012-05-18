// -----------------------------------------------------------------------
// <copyright file="RegisterTypesConfiguration.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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
    /// The register types configuration options
    /// </summary>
    public class RegisterTypesConfiguration : IRegisterTypesConfiguration
    {
        /// <summary>
        /// Registers the types.
        /// </summary>
        public void RegisterTypes()
        {
            AutoRegister.RegisterTypes();
        }
    }
}
