//-----------------------------------------------------------------------
// <copyright file="EntityValidator.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ServiceLayer.ServiceImplementation
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Defines the .<see cref="EntityValidator" />
    /// </summary>
    public static class EntityValidator
    {
        /// <summary>
        /// The IsEntityValid.
        /// </summary>
        /// <param name="entity">The entity.<see cref="IValidatableObject"/></param>
        /// <returns>The .<see cref="IList{ValidationResult}"/></returns>
        public static IList<ValidationResult> IsEntityValid(IValidatableObject entity)
        {
            var context = new ValidationContext(entity);
            IList<ValidationResult> results = new List<ValidationResult>();
            Validator.TryValidateObject(entity, context, results, true);
            return results;
        }
    }
}
