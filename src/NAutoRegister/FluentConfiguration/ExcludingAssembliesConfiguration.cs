// -----------------------------------------------------------------------
// <copyright file="ExcludingAssembliesConfiguration.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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
    using System.Reflection;
    using CuttingEdge.Conditions;

    /// <summary>
    /// Excluding assemblies configuration options
    /// </summary>
    public class ExcludingAssembliesConfiguration : IExcludingAssembliesConfiguration
    {
        /// <summary>
        /// Contains a collection of all the excluded assemblies registered
        /// </summary>
        private List<Assembly> excludedAssemblies;

        /// <summary>
        /// Contains a collection of all the excluded assembly names registered
        /// </summary>
        private List<string> excludedAssemblyNames;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExcludingAssembliesConfiguration"/> class.
        /// </summary>
        public ExcludingAssembliesConfiguration()
        {
            this.excludedAssemblies = new List<Assembly>();
            this.excludedAssemblyNames = new List<string>();
        }

        /// <summary>
        /// Gets the excluded assemblies.
        /// </summary>
        public IEnumerable<Assembly> ExcludedAssemblies
        {
            get { return this.excludedAssemblies.AsEnumerable(); }
        }

        /// <summary>
        /// Gets the excluded assembly names.
        /// </summary>
        public IEnumerable<string> ExcludedAssemblyNames
        {
            get { return this.excludedAssemblyNames.AsEnumerable(); }
        }

        /// <summary>
        /// Gets the including assembly configuration.
        /// </summary>
        public IIncludingAssembliesConfiguration Including
        {
            get { return AutoRegister.IncludingAssembliesConfiguration; }
        }

        /// <summary>
        /// Gets the mappings configuration.
        /// </summary>
        public IMappingsConfiguration WithMappings
        {
            get { return AutoRegister.MappingsConfiguration; }
        }

        /// <summary>
        /// Adds the specified assemblies to the excluded assmeblies collection
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>
        /// <returns>
        /// The current instance of the excluding assemblies configuration
        /// </returns>
        public IExcludingAssembliesConfiguration Assemblies(IEnumerable<Assembly> assemblies)
        {
            Condition.Requires(assemblies).IsNotNull();

            this.excludedAssemblies.AddRange(assemblies);
            return this;
        }

        /// <summary>
        /// Adds the specified assembly name to the excluded assembly names collection
        /// </summary>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <returns>
        /// The current instance of the excluding assemblies configuration
        /// </returns>
        public IExcludingAssembliesConfiguration Assembly(string assemblyName)
        {
            Condition.Requires(assemblyName).IsNotNullOrWhiteSpace();

            this.excludedAssemblyNames.Add(assemblyName);
            return this;
        }

        /// <summary>
        /// Adds the specified assembly to the excluded assemblies collection
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns>
        /// The current instance of the excluding assemblies configuration
        /// </returns>
        public IExcludingAssembliesConfiguration Assembly(Assembly assembly)
        {
            Condition.Requires(assembly).IsNotNull();

            this.excludedAssemblies.Add(assembly);
            return this;
        }

        /// <summary>
        /// Adds the assembly of the generic type to the excluded assemblies collection
        /// </summary>
        /// <typeparam name="T">The type to get the assembly from</typeparam>
        /// <returns>
        /// The current instance of the excluding assemblies configuration
        /// </returns>
        public IExcludingAssembliesConfiguration AssemblyFromType<T>()
        {
            this.excludedAssemblies.Add(typeof(T).Assembly);
            return this;
        }

        /// <summary>
        /// Adds the assembly of the type parameter to the excluded assemblies collection
        /// </summary>
        /// <param name="type">The type to get the assmebly from.</param>
        /// <returns>
        /// The current instance of the excluding assemblies configuration
        /// </returns>
        public IExcludingAssembliesConfiguration AssemblyFromType(Type type)
        {
            Condition.Requires(type).IsNotNull();

            this.excludedAssemblies.Add(type.Assembly);
            return this;
        }
    }
}
