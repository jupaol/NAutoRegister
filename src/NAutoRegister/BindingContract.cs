// -----------------------------------------------------------------------
// <copyright file="BindingContract.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CuttingEdge.Conditions;

    /// <summary>
    /// Represents the current binding type (contract type) used to automatically resolve the 
    /// implementing types. It also contains all the <see cref="BindingCondition"/> objects
    /// associated with this contract type
    /// </summary>
    public class BindingContract
    {
        /// <summary>
        /// Contains all the <see cref="BindingCondition"/> objects associated to the
        /// current contract type
        /// </summary>
        private IList<BindingCondition> bindingConditions;

        /// <summary>
        /// Initializes a new instance of the <see cref="BindingContract"/> class.
        /// </summary>
        /// <param name="currentContractType">Type of the current contract.</param>
        public BindingContract(Type currentContractType)
        {
            Condition.Requires(currentContractType).IsNotNull();

            this.bindingConditions = new List<BindingCondition>();
            this.CurrentContractType = currentContractType;
        }

        /// <summary>
        /// Gets the type of the current contract.
        /// </summary>
        /// <value>
        /// The type of the current contract.
        /// </value>
        public Type CurrentContractType { get; private set; }

        /// <summary>
        /// Gets the binding conditions.
        /// </summary>
        public IEnumerable<BindingCondition> BindingConditions
        {
            get
            {
                return this.bindingConditions;
            }
        }

        /// <summary>
        /// Adds the binding condition.
        /// </summary>
        /// <param name="evaluateCondition">The evaluate condition.</param>
        /// <returns>
        /// Returns the new <see cref="BindingCondition"/> created
        /// </returns>
        public BindingCondition AddBindingCondition(Func<Type, bool> evaluateCondition)
        {
            Condition.Requires(evaluateCondition).IsNotNull();

            var bindingCondition = new RejectBindingCondition(this, evaluateCondition);

            this.bindingConditions.Add(bindingCondition);

            return bindingCondition;
        }

        /// <summary>
        /// Gets the implementing types.
        /// </summary>
        /// <param name="types">The loaded types used as the source of the query to get the implementing types.</param>
        /// <returns>
        /// Returns the filtered implementing types of the current contract type
        /// </returns>
        public IEnumerable<Type> GetImplementingTypes(params Type[] types)
        {
            Condition.Requires(types).IsNotNull();

            var implementingTypes = this.ResolveImplementingTypes(types);
            return this.FilterTypes(implementingTypes);
        }

        /// <summary>
        /// Apply all the <see cref="BindingCondition"/> conditions asscociated to the current
        /// contract type
        /// </summary>
        /// <param name="implementingTypes">The implementing types associated to the current contract type.</param>
        /// <returns>
        /// Returns the filtered implementing types of the current contract type
        /// </returns>
        private IEnumerable<Type> FilterTypes(IEnumerable<Type> implementingTypes)
        {
            Condition.Requires(implementingTypes).IsNotNull();

            IList<Type> processedTypes = new List<Type>();

            foreach (var implementingType in implementingTypes)
            {
                var isBindable = true;

                foreach (var bindingCondition in this.bindingConditions)
                {
                    if (bindingCondition.EvaluateCondition(implementingType))
                    {
                        isBindable = false;
                        break;
                    }
                }

                if (isBindable)
                {
                    processedTypes.Add(implementingType);
                }
            }

            return processedTypes.AsEnumerable();
        }

        /// <summary>
        /// Resolves the implementing types.
        /// </summary>
        /// <remarks>
        /// Query through all the specified types to find all the implementing types for the
        /// current contract type
        /// </remarks>
        /// <param name="types">The loaded types used as the source of the query to get the implementing types.</param>
        /// <returns>
        /// All the implementing types for the current contract type
        /// </returns>
        private IEnumerable<Type> ResolveImplementingTypes(IEnumerable<Type> types)
        {
            Condition.Requires(types).IsNotNull();

            var query = types;

            if (this.CurrentContractType.IsGenericType)
            {
                query = from type in query
                        from implementingType in type.GetInterfaces()
                        where implementingType.IsGenericType
                        let genericTypeDefinition = this.CurrentContractType.IsGenericTypeDefinition ?
                            this.CurrentContractType :
                            this.CurrentContractType.GetGenericTypeDefinition()
                        where genericTypeDefinition == implementingType.GetGenericTypeDefinition()
                        select type;

                if (!this.CurrentContractType.IsGenericTypeDefinition)
                {
                    query = query.Where(x => this.CurrentContractType.IsAssignableFrom(x));
                }
            }
            else
            {
                query = from type in query
                        from implementingType in type.GetInterfaces()
                        where !implementingType.IsGenericType
                        where implementingType == this.CurrentContractType
                        where implementingType.IsAssignableFrom(this.CurrentContractType)
                        select type;
            }

            return query.Distinct();
        }
    }
}
