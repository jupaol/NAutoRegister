// -----------------------------------------------------------------------
// <copyright file="AssemblyMerger.cs" company="Juan Pablo Olmos Lara (Jupaol)">
//
// jupaol@hotmail.com
// http://jupaol.blogspot.com/
// 
// Copyright (c) 2012, Juan Pablo Olmos Lara (Jupaol)
// All rights reserved.
// 
// </copyright>
// -----------------------------------------------------------------------

namespace NAutoRegister
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using CuttingEdge.Conditions;

    /// <summary>
    /// The assembly merger object. This class is in charge to merge the assemblies to include
    /// removing the assemblies to exclude specified by the user
    /// </summary>
    public class AssemblyMerger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyMerger"/> class.
        /// </summary>
        /// <param name="assembliesToInclude">The assemblies to include.</param>
        /// <param name="assembliesToExclude">The assemblies to exclude.</param>
        /// <param name="assemblyNamesToExclude">The assembly names to exclude.</param>
        public AssemblyMerger(
            IEnumerable<Assembly> assembliesToInclude, 
            IEnumerable<Assembly> assembliesToExclude,
            IEnumerable<string> assemblyNamesToExclude)
        {
            Condition.Requires(assembliesToInclude).IsNotNull();
            Condition.Requires(assembliesToExclude).IsNotNull();
            Condition.Requires(assemblyNamesToExclude).IsNotNull();

            this.AssembliesToExclude = assembliesToExclude;
            this.AssembliesToInclude = assembliesToInclude;
            this.AssemblyNamesToExclude = assemblyNamesToExclude;
        }

        /// <summary>
        /// Gets the assemblies to include.
        /// </summary>
        public IEnumerable<Assembly> AssembliesToInclude { get; private set; }

        /// <summary>
        /// Gets the assemblies to exclude.
        /// </summary>
        public IEnumerable<Assembly> AssembliesToExclude { get; private set; }

        /// <summary>
        /// Gets the assembly names to exclude.
        /// </summary>
        public IEnumerable<string> AssemblyNamesToExclude { get; private set; }

        /// <summary>
        /// Merges the assemblies.
        /// </summary>
        /// <returns>
        /// The merged assemblies
        /// </returns>
        public IEnumerable<Assembly> MergeAssemblies()
        {
            var mergedAssemblies = this.AssembliesToInclude.ToList();

            this.AssembliesToExclude.ToList().ForEach(x => mergedAssemblies.Remove(x));
            this.AssemblyNamesToExclude.ToList().ForEach(x => mergedAssemblies.RemoveAll(c => c.FullName == x));

            return mergedAssemblies.AsEnumerable();
        }
    }
}
