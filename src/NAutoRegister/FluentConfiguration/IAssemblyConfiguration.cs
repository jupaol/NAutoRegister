// -----------------------------------------------------------------------
// <copyright file="IAssemblyConfiguration.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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
    /// Assembly configuration contract
    /// </summary>
    public interface IAssemblyConfiguration
    {
        /// <summary>
        /// Gets the including assembly configuration options.
        /// </summary>
        IIncludingAssembliesConfiguration Including { get; }

        /// <summary>
        /// Gets the excluding assembly configuration options.
        /// </summary>
        IExcludingAssembliesConfiguration Excluding { get; }
    }
}
