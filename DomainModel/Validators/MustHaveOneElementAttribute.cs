//-----------------------------------------------------------------------
// <copyright file="MustHaveOneElementAttribute.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DomainModel.Validators
{
    using System.Collections;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Defines the .<see cref="MustHaveOneElementAttribute" />
    /// </summary>
    public sealed class MustHaveOneElementAttribute : ValidationAttribute
    {
        /// <summary>
        /// The IsValid.
        /// </summary>
        /// <param name="value">The value.<see cref="object"/></param>
        /// <returns>The .<see cref="bool"/></returns>
        public override bool IsValid(object value)
        {
            var list = (IList)value;
            if (list != null)
            {
                return list.Count > 0;
            }

            return false;
        }
    }
}
