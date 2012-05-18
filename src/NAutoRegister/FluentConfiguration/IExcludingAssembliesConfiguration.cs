// -----------------------------------------------------------------------
// <copyright file="IExcludingAssembliesConfiguration.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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
    using System.Reflection;

    /// <summary>
    /// Excluding assembly configuration contract
    /// </summary>
    public interface IExcludingAssembliesConfiguration
    {
        /// <summary>
        /// Gets the excluded assemblies.
        /// </summary>
        IEnumerable<Assembly> ExcludedAssemblies { get; }

        /// <summary>
        /// Gets the excluded assembly names.
        /// </summary>
        IEnumerable<string> ExcludedAssemblyNames { get; }

        /// <summary>
        /// Gets the including assembly configuration options.
        /// </summary>
        IIncludingAssembliesConfiguration Including { get; }

        /// <summary>
        /// Adds the specified assemblies to the excluded assmeblies collection
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>
        /// <returns>
        /// The current instance of the excluding assemblies configuration
        /// </returns>
        IExcludingAssembliesConfiguration Assemblies(IEnumerable<Assembly> assemblies);

        /// <summary>
        /// Adds the specified assembly name to the excluded assembly names collection
        /// </summary>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <returns>
        /// The current instance of the excluding assemblies configuration
        /// </returns>
        IExcludingAssembliesConfiguration Assembly(string assemblyName);

        /// <summary>
        /// Adds the specified assembly to the excluded assemblies collection
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns>
        /// The current instance of the excluding assemblies configuration
        /// </returns>
        IExcludingAssembliesConfiguration Assembly(Assembly assembly);

        /// <summary>
        /// Adds the assembly of the generic type to the excluded assemblies collection
        /// </summary>
        /// <typeparam name="T">The type to get the assembly from</typeparam>
        /// <returns>
        /// The current instance of the excluding assemblies configuration
        /// </returns>
        IExcludingAssembliesConfiguration AssemblyFromType<T>();

        /// <summary>
        /// Adds the assembly of the type parameter to the excluded assemblies collection
        /// </summary>
        /// <param name="type">The type to get the assmebly from.</param>
        /// <returns>
        /// The current instance of the excluding assemblies configuration
        /// </returns>
        IExcludingAssembliesConfiguration AssemblyFromType(Type type);
    }
}
