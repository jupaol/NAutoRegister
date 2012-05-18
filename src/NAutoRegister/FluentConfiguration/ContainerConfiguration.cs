// -----------------------------------------------------------------------
// <copyright file="ContainerConfiguration.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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
    using CuttingEdge.Conditions;

    /// <summary>
    /// The container configuration options
    /// </summary>
    public class ContainerConfiguration : IContainerConfiguration
    {
        /// <summary>
        /// Gets the current container.
        /// </summary>
        public IContainer CurrentContainer { get; private set; }

        /// <summary>
        /// Adds the container.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <returns>
        /// An instance of the register types configuration object
        /// </returns>
        public IRegisterTypesConfiguration Container(IContainer container)
        {
            Condition.Requires(container).IsNotNull();

            this.CurrentContainer = container;

            return AutoRegister.RegisterTypesConfiguration;
        }
    }
}
