// -----------------------------------------------------------------------
// <copyright file="IncludingAssembliesConfiguration.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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
    /// Including assemblies configuration options
    /// </summary>
    public class IncludingAssembliesConfiguration : IIncludingAssembliesConfiguration
    {
        /// <summary>
        /// Contains all the included assemblies registered
        /// </summary>
        private List<Assembly> includedAssemblies;

        /// <summary>
        /// Initializes a new instance of the <see cref="IncludingAssembliesConfiguration"/> class.
        /// </summary>
        public IncludingAssembliesConfiguration()
        {
            this.includedAssemblies = new List<Assembly>();
        }

        /// <summary>
        /// Gets the included assemblies.
        /// </summary>
        public IEnumerable<Assembly> IncludedAssemblies
        {
            get { return this.includedAssemblies.AsEnumerable(); }
        }

        /// <summary>
        /// Gets the exccluding assemblies configuration options
        /// </summary>
        public IExcludingAssembliesConfiguration Excluding
        {
            get { return AutoRegister.ExcludingAssembliesConfiguration; }
        }

        /// <summary>
        /// Adds the specified assembly to the included assemblies collection
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns>
        /// The current instance of the including assemblies configuration
        /// </returns>
        public IIncludingAssembliesConfiguration Assembly(Assembly assembly)
        {
            Condition.Requires(assembly).IsNotNull();

            this.includedAssemblies.Add(assembly);
            return this;
        }

        /// <summary>
        /// Adds all the specified assemblies to the included assmeblies collection
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>
        /// <returns>
        /// The current instance of the including assemblies configuration
        /// </returns>
        public IIncludingAssembliesConfiguration Assemblies(IEnumerable<Assembly> assemblies)
        {
            Condition.Requires(assemblies).IsNotNull();

            this.includedAssemblies.AddRange(assemblies);
            return this;
        }

        /// <summary>
        /// Adds the assembly of the generic type to the included assemblies collection
        /// </summary>
        /// <typeparam name="T">The generic type to get the assembly from</typeparam>
        /// <returns>
        /// The current instance of the including assemblies configuration
        /// </returns>
        public IIncludingAssembliesConfiguration AssemblyFromType<T>()
        {
            this.includedAssemblies.Add(typeof(T).Assembly);
            return this;
        }

        /// <summary>
        /// Adds the assembly of the type parameter to the included assemblies collection
        /// </summary>
        /// <param name="type">The type to get the assembly from.</param>
        /// <returns>
        /// The current instance of the including assemblies configuration
        /// </returns>
        public IIncludingAssembliesConfiguration AssemblyFromType(Type type)
        {
            Condition.Requires(type).IsNotNull();

            this.includedAssemblies.Add(type.Assembly);
            return this;
        }
    }
}
