// -----------------------------------------------------------------------
// <copyright file="AutoRegister.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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
    using System.Linq;

    /// <summary>
    /// NAutoRegister configuration object
    /// </summary>
    public static class AutoRegister
    {
        /// <summary>
        /// Initializes static members of the <see cref="AutoRegister"/> class.
        /// </summary>
        static AutoRegister()
        {
            Initialize();
        }

        /// <summary>
        /// Gets the assembly configuration.
        /// </summary>
        internal static IAssemblyConfiguration AssemblyConfiguration { get; private set; }

        /// <summary>
        /// Gets the including assemblies configuration.
        /// </summary>
        internal static IIncludingAssembliesConfiguration IncludingAssembliesConfiguration { get; private set; }

        /// <summary>
        /// Gets the excluding assemblies configuration.
        /// </summary>
        internal static IExcludingAssembliesConfiguration ExcludingAssembliesConfiguration { get; private set; }

        /// <summary>
        /// Gets the mappings configuration.
        /// </summary>
        internal static IMappingsConfiguration MappingsConfiguration { get; private set; }

        /// <summary>
        /// Gets the specific mappings configuration.
        /// </summary>
        internal static ISpecificMappingsConfiguration SpecificMappingsConfiguration { get; private set; }

        /// <summary>
        /// Gets the specific mappings for type configuration
        /// </summary>
        /// <value>
        /// The type of the specific mappings for type configuration
        /// </value>
        internal static ISpecificMappingsConfigurationForType SpecificMappingsConfigurationForType { get; private set; }

        /// <summary>
        /// Starts the configuration process
        /// </summary>
        /// <returns>
        /// The initial assembly configuration options
        /// </returns>
        public static IAssemblyConfiguration Configure()
        {
            return AutoRegister.AssemblyConfiguration;
        }

        /// <summary>
        /// Registers the specified types
        /// </summary>
        internal static void RegisterTypes()
        {
            var assemblyMerger = new AssemblyMerger(
                AutoRegister.IncludingAssembliesConfiguration.IncludedAssemblies,
                AutoRegister.ExcludingAssembliesConfiguration.ExcludedAssemblies,
                AutoRegister.ExcludingAssembliesConfiguration.ExcludedAssemblyNames);
            var mergedAssemblies = assemblyMerger.MergeAssemblies();
            var bindingProcessor = new BindingProcessor(mergedAssemblies);

            AutoRegister.SpecificMappingsConfigurationForType.BindingContracts.ToList().ForEach(x => bindingProcessor.AddBindingContract(x));

            ////bindingProcessor.RegisterTypes();
        }

        /// <summary>
        /// Initializes the NAutoRegister configuration
        /// </summary>
        private static void Initialize()
        {
            AutoRegister.AssemblyConfiguration = new AssemblyConfiguration();
            AutoRegister.IncludingAssembliesConfiguration = new IncludingAssembliesConfiguration();
            AutoRegister.ExcludingAssembliesConfiguration = new ExcludingAssembliesConfiguration();
            AutoRegister.MappingsConfiguration = new MappingsConfiguration();
            AutoRegister.SpecificMappingsConfiguration = new SpecificMappingsConfiguration();
            AutoRegister.SpecificMappingsConfigurationForType = new SpecificMappingsConfigurationForType();
        }
    }
}
