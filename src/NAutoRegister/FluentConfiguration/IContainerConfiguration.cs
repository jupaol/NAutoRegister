// -----------------------------------------------------------------------
// <copyright file="IContainerConfiguration.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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
    /// The container configuration contract
    /// </summary>
    public interface IContainerConfiguration
    {
        /// <summary>
        /// Gets the current container.
        /// </summary>
        IContainer CurrentContainer { get; }

        /// <summary>
        /// Adds the container.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <returns>
        /// An instance of the register types configuration object
        /// </returns>
        IRegisterTypesConfiguration AddContainer(IContainer container);
    }
}
