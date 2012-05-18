// -----------------------------------------------------------------------
// <copyright file="BindingProcessor.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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
    using System.Reflection;
    using CuttingEdge.Conditions;

    /// <summary>
    /// Processes all the specified <see cref="BindingContract"/> objects
    /// </summary>
    public class BindingProcessor
    {
        /// <summary>
        /// Contains all the <see cref="BindingContract"/> objects specified and ready to be 
        /// registered into the specified <see cref="IContainer"/>
        /// </summary>
        private List<BindingContract> bindingContracts;

        /// <summary>
        /// Contains all the loaded types contained in all the assemblies specified
        /// </summary>
        private List<Type> loadedTypes;

        /// <summary>
        /// Initializes a new instance of the <see cref="BindingProcessor"/> class.
        /// </summary>
        /// <param name="assemblies">All the loaded assemblies.</param>
        public BindingProcessor(IEnumerable<Assembly> assemblies)
        {
            Condition.Requires(assemblies).IsNotNull();

            this.bindingContracts = new List<BindingContract>();
            this.loadedTypes = this.ResolveTypes(assemblies).ToList();
        }

        /// <summary>
        /// Gets or sets a value indicating whether allow to register duplicated types.
        /// Default value is <c>false</c>
        /// </summary>
        /// <remarks>
        /// If this property is set to <c>true</c>, when calling <c>RegisterTypes</c> an
        /// <see cref="InvalidOperationException"/> exception will be thrown when the
        /// type to register already exists in the <see cref="IContainer"/> specified
        /// </remarks>
        /// <value>
        ///    <c>true</c> if allow to register duplicated types; otherwise, <c>false</c>.
        /// </value>
        public bool AllowRegisterDuplicatedTypes { get; set; }

        /// <summary>
        /// Adds the binding contract.
        /// </summary>
        /// <param name="bindingContract">The binding contract.</param>
        /// <returns>
        /// The <see cref="BindingContract"/> object added
        /// </returns>
        public BindingContract AddBindingContract(BindingContract bindingContract)
        {
            Condition.Requires(bindingContract).IsNotNull();

            this.CheckBindingContract(bindingContract);
            this.bindingContracts.Add(bindingContract);

            return bindingContract;
        }

        /// <summary>
        /// Registers the types into the specified <see cref="IContainer"/>.
        /// </summary>
        /// <remarks>
        /// If the <c>AllowRegisterDuplicatedTypes</c> property is set to <c>true</c>, an
        /// <see cref="InvalidOperationException"/> exception will be thrown when the
        /// type to register already exists in the <see cref="IContainer"/> specified
        /// </remarks>
        /// <param name="container">The container.</param>
        public void RegisterTypes(IContainer container)
        {
            Condition.Requires(container).IsNotNull();

            foreach (var bindingContract in this.bindingContracts)
            {
                foreach (var implementingType in bindingContract.GetImplementingTypes(this.loadedTypes.ToArray()))
                {
                    if (!this.AllowRegisterDuplicatedTypes)
                    {
                        // TODO: check if objects exist
                    }

                    if (!bindingContract.CurrentContractType.IsGenericType)
                    {
                        container.Register(bindingContract.CurrentContractType, implementingType);
                    }

                    if (bindingContract.CurrentContractType.IsGenericType
                        && !bindingContract.CurrentContractType.IsGenericTypeDefinition)
                    {
                        container.Register(bindingContract.CurrentContractType, implementingType);
                    }

                    if (bindingContract.CurrentContractType.IsGenericType
                        && bindingContract.CurrentContractType.IsGenericTypeDefinition)
                    {
                        var newContractType = implementingType.GetInterface(bindingContract.CurrentContractType.Name);

                        container.Register(newContractType, implementingType);
                    }
                }
            }
        }

        /// <summary>
        /// Validates the binding contract to determine if it can be added.
        /// </summary>
        /// <param name="bindingContract">The binding contract.</param>
        private void CheckBindingContract(BindingContract bindingContract)
        {
            if (this.bindingContracts.Any(x => x.CurrentContractType == bindingContract.CurrentContractType))
            {
                throw new InvalidOperationException(string.Format("The binding contract for the type: '{0}' was already registered", bindingContract.CurrentContractType.FullName));
            }
        }

        /// <summary>
        /// Resolves the all the types from all the specified assemblies
        /// </summary>
        /// <param name="assemblies">The loaded assemblies.</param>
        /// <returns>
        /// All the types contained in all the assemblies specified
        /// </returns>
        private IEnumerable<Type> ResolveTypes(IEnumerable<Assembly> assemblies)
        {
            var types = new List<Type>();

            assemblies.ToList().ForEach(x => types.AddRange(x.GetTypes()));

            return types.AsEnumerable();
        }
    }
}
