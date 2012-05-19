// -----------------------------------------------------------------------
// <copyright file="NinjectContainerProvider.cs" company="Juan Pablo Olmos Lara (Jupaol)">
//
// jupaol@hotmail.com
// http://jupaol.blogspot.com/
// 
// Copyright (c) 2012, Juan Pablo Olmos Lara (Jupaol)
// All rights reserved.
// 
// </copyright>
// -----------------------------------------------------------------------

namespace NAutoRegister.NinjectProvider
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Ninject;

    /// <summary>
    /// The Ninject container provider
    /// </summary>
    public class NinjectContainerProvider : IContainer
    {
        /// <summary>
        /// The Ninject kernel (container)
        /// </summary>
        private IKernel kernel;

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectContainerProvider"/> class.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        public NinjectContainerProvider(IKernel kernel)
        {
            if (kernel == null)
            {
                throw new ArgumentNullException("kernel");
            }

            this.kernel = kernel;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectContainerProvider"/> class.
        /// </summary>
        public NinjectContainerProvider()
            : this(new StandardKernel())
        {
        }

        /// <summary>
        /// Resolves the specified type
        /// </summary>
        /// <typeparam name="T">The type to resolve</typeparam>
        /// <returns>
        /// The resolved type
        /// </returns>
        public T Resolve<T>() where T : class
        {
            return this.kernel.Get<T>();
        }

        /// <summary>
        /// Resolves the specified type.
        /// </summary>
        /// <param name="type">The type to resolve.</param>
        /// <returns>
        /// The resolved type
        /// </returns>
        public object Resolve(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            return this.kernel.Get(type);
        }

        /// <summary>
        /// Registers this instance.
        /// </summary>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        public void Register<TContract, TImplementation>() where TImplementation : class, TContract
        {
            this.kernel.Bind<TContract>().To<TImplementation>();
        }

        /// <summary>
        /// Registers the implementation type with the contract type specified
        /// </summary>
        /// <param name="contractType">Type of the contract.</param>
        /// <param name="implementationType">Type of the implementation.</param>
        public void Register(Type contractType, Type implementationType)
        {
            if (contractType == null)
            {
                throw new ArgumentNullException("contractType");
            }

            if (implementationType == null)
            {
                throw new ArgumentNullException("implementationType");
            }

            this.kernel.Bind(contractType).To(implementationType);
        }
    }
}
