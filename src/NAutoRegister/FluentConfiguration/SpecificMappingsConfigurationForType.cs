// -----------------------------------------------------------------------
// <copyright file="SpecificMappingsConfigurationForType.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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
    /// The configuration to be used when the user wants to register a new type
    /// </summary>
    public class SpecificMappingsConfigurationForType : ISpecificMappingsConfigurationForType
    {
        /// <summary>
        /// Contains all the binding contracts specified by the user
        /// </summary>
        private List<BindingContract> bindingContracts;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecificMappingsConfigurationForType"/> class.
        /// </summary>
        public SpecificMappingsConfigurationForType()
        {
            this.bindingContracts = new List<BindingContract>();
        }

        /// <summary>
        /// Gets the binding contracts.
        /// </summary>
        public IEnumerable<BindingContract> BindingContracts
        {
            get { return this.bindingContracts.AsEnumerable(); }
        }

        /// <summary>
        /// Starts the mappings configuration for the specified type
        /// </summary>
        /// <typeparam name="T">The contract type (base type, usually an interface or an abstract class) to configure.</typeparam>
        /// <param name="evaluateCondition">The evaluate condition. This condition will be applied to all the types implementing
        /// or inheriting the <c>T</c> type specified. This condition will determine if the
        /// implementing type being evaluated should be auto registered as an implementation
        /// of the <c>T</c> type specified</param>
        /// <returns>
        /// An instance of the <see cref="ISpecificMappingsConfiguration"/> specific mappings
        /// configuration object
        /// </returns>
        public ISpecificMappingsConfiguration Type<T>(params Func<Type, bool>[] evaluateCondition)
        {
            this.Type(typeof(T), evaluateCondition);

            return AutoRegister.SpecificMappingsConfiguration;
        }

        /// <summary>
        /// Starts the mappings configuration for the specified type
        /// </summary>
        /// <param name="type">The contract type (base type, usually an interface or an abstract class) to configure.</param>
        /// <param name="evaluateCondition">The evaluate condition. This condition will be applied to all the types implementing
        /// or inheriting the <c>type</c> type specified. This condition will determine if the
        /// implementing type being evaluated should be auto registered as an implementation
        /// of the <c>type</c> type specified</param>
        /// <returns>
        /// An instance of the <see cref="ISpecificMappingsConfiguration"/> specific mappings
        /// configuration object
        /// </returns>
        public ISpecificMappingsConfiguration Type(Type type, params Func<Type, bool>[] evaluateCondition)
        {
            Condition.Requires(type).IsNotNull();
            Condition.Requires(evaluateCondition).IsNotNull();
            Condition.Requires(evaluateCondition).DoesNotContain(null);

            var bindingContract = new BindingContract(type);

            evaluateCondition.ToList().ForEach(x => bindingContract.AddBindingCondition(x));
            this.bindingContracts.Add(bindingContract);

            return AutoRegister.SpecificMappingsConfiguration;
        }
    }
}
