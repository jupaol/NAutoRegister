// -----------------------------------------------------------------------
// <copyright file="IIncludingAssembliesConfiguration.cs" company="Hewlett-Packard">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace NAutoRegister.FluentConfiguration
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Including assemblies configuration contract
    /// </summary>
    public interface IIncludingAssembliesConfiguration
    {
        /// <summary>
        /// Gets the included assemblies.
        /// </summary>
        IEnumerable<Assembly> IncludedAssemblies { get; }

        /// <summary>
        /// Gets the exccluding assemblies configuration options
        /// </summary>
        IExcludingAssembliesConfiguration Excluding { get; }

        /// <summary>
        /// Adds the specified assembly to the included assemblies collection
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns>
        /// The current instance of the including assemblies configuration
        /// </returns>
        IIncludingAssembliesConfiguration Assembly(Assembly assembly);

        /// <summary>
        /// Adds all the specified assemblies to the included assmeblies collection
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>
        /// <returns>
        /// The current instance of the including assemblies configuration
        /// </returns>
        IIncludingAssembliesConfiguration Assemblies(IEnumerable<Assembly> assemblies);

        /// <summary>
        /// Adds the assembly of the generic type to the included assemblies collection
        /// </summary>
        /// <typeparam name="T">The generic type to get the assembly from</typeparam>
        /// <returns>
        /// The current instance of the including assemblies configuration
        /// </returns>
        IIncludingAssembliesConfiguration AssemblyFromType<T>();

        /// <summary>
        /// Adds the assembly of the type parameter to the included assemblies collection
        /// </summary>
        /// <param name="type">The type to get the assembly from.</param>
        /// <returns>
        /// The current instance of the including assemblies configuration
        /// </returns>
        IIncludingAssembliesConfiguration AssemblyFromType(Type type);
    }
}
