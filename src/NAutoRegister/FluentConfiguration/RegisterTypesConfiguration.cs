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
        /// Gets the current container.
        /// </summary>
        public IContainer CurrentContainer
        {
            get { return AutoRegister.ContainerConfiguration.CurrentContainer; }
        }

        /// <summary>
        /// Registers the types.
        /// </summary>
        /// <returns>
        /// The current <see cref="IRegisterTypesConfiguration"/> instance
        /// </returns>
        public IRegisterTypesConfiguration RegisterTypes()
        {
            AutoRegister.RegisterTypes();

            return this;
        }

        /// <summary>
        /// Resets the configuration.
        /// </summary>
        public void ResetConfiguration()
        {
            AutoRegister.ResetConfiguration();
        }
    }
}
