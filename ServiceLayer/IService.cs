//-----------------------------------------------------------------------
// <copyright file="IService.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ServiceLayer
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Defines the .<see cref="IService{T}" />.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    public interface IService<T>
    where T : class
    {
        /// <summary>
        /// The Insert.
        /// </summary>
        /// <param name="entity">The entity.<see cref="T"/>.</param>
        /// <returns>The .<see cref="IList{ValidationResult}"/>.</returns>
        IList<ValidationResult> Insert(T entity);

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="entity">The entity.<see cref="T"/>.</param>
        /// <returns>The .<see cref="IList{ValidationResult}"/>.</returns>
        IList<ValidationResult> Update(T entity);

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="entity">The entity.<see cref="T"/>.</param>
        void Delete(T entity);

        /// <summary>
        /// The GetByID.
        /// </summary>
        /// <param name="id">The id.<see cref="object"/>.</param>
        /// <returns>The .<see cref="T"/>.</returns>
        T GetByID(object id);

        /// <summary>
        /// The GetAll.
        /// </summary>
        /// <returns>The .<see cref="IEnumerable{T}"/>.</returns>
        IEnumerable<T> GetAll();
    }
}
