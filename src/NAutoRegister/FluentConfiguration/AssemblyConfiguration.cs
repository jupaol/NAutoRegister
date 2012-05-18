// -----------------------------------------------------------------------
// <copyright file="AssemblyConfiguration.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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
    /// Assembly configuration options
    /// </summary>
    public class AssemblyConfiguration : IAssemblyConfiguration
    {
        /// <summary>
        /// Gets the including assembly configuration options.
        /// </summary>
        public IIncludingAssembliesConfiguration Including
        {
            get { return AutoRegister.IncludingAssembliesConfiguration; }
        }

        /// <summary>
        /// Gets the excluding assembly configuration options.
        /// </summary>
        public IExcludingAssembliesConfiguration Excluding
        {
            get { return AutoRegister.ExcludingAssembliesConfiguration; }
        }
    }
}
