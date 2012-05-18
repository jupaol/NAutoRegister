// -----------------------------------------------------------------------
// <copyright file="ISpecificMappingsConfiguration.cs" company="Hewlett-Packard">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace NAutoRegister.FluentConfiguration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// The specific mappings configuration contract
    /// </summary>
    public interface ISpecificMappingsConfiguration
    {
        /// <summary>
        /// Gets the mappings configuration.
        /// </summary>
        IMappingsConfiguration WithMappings { get; }
    }
}
