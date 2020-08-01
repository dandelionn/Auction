//-----------------------------------------------------------------------
// <copyright file="IRepository.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DataMapper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Defines the <see cref="IRepository{T}" />.
    /// </summary>
    /// <typeparam name="T">The element type of the interface.</typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// The Insert.
        /// </summary>
        /// <param name="entity">The entity<see cref="T"/>.</param>
        void Insert(T entity);

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="item">The item<see cref="T"/>.</param>
        void Update(T item);

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="entity">The entity<see cref="T"/>.</param>
        void Delete(T entity);

        /// <summary>
        /// The GetByID.
        /// </summary>
        /// <param name="id">The id<see cref="object"/>.</param>
        /// <returns>The <see cref="T"/>.</returns>
        T GetByID(object id);

        /// <summary>
        /// The Get.
        /// </summary>
        /// <param name="filter">The filter<see cref="Expression{Func{T, bool}}"/>.</param>
        /// <param name="orderBy">The orderBy<see cref="Func{IQueryable{T}, IOrderedQueryable{T}}"/>.</param>
        /// <param name="includeProperties">The includeProperties<see cref="string"/>.</param>
        /// <returns>The <see cref="IEnumerable{T}"/>.</returns>
        IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");
    }
}
