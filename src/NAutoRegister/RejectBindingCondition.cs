// -----------------------------------------------------------------------
// <copyright file="RejectBindingCondition.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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

    /// <summary>
    /// Condition used to REJECT the implementing types
    /// </summary>
    public class RejectBindingCondition : BindingCondition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RejectBindingCondition"/> class.
        /// </summary>
        /// <param name="bindingContract">The binding contract.</param>
        /// <param name="evaluateCondition">The evaluate condition.</param>
        public RejectBindingCondition(BindingContract bindingContract, Func<Type, bool> evaluateCondition)
            : base(bindingContract, evaluateCondition)
        {
        }
    }
}
