// -----------------------------------------------------------------------
// <copyright file="BindingCondition.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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
    using CuttingEdge.Conditions;

    /// <summary>
    /// Base binding condition
    /// </summary>
    public abstract class BindingCondition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BindingCondition"/> class.
        /// </summary>
        /// <param name="bindingContract">The binding contract.</param>
        /// <param name="evaluateCondition">The evaluate condition.</param>
        public BindingCondition(BindingContract bindingContract, Func<Type, bool> evaluateCondition)
        {
            Condition.Requires(evaluateCondition).IsNotNull();
            Condition.Requires(bindingContract).IsNotNull();

            this.EvaluateCondition = evaluateCondition;
            this.BindingContract = bindingContract;
        }

        /// <summary>
        /// Gets the evaluate condition.
        /// </summary>
        public Func<Type, bool> EvaluateCondition { get; private set; }

        /// <summary>
        /// Gets the binding contract.
        /// </summary>
        public BindingContract BindingContract { get; private set; }
    }
}
