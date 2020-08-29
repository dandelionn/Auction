//-----------------------------------------------------------------------
// <copyright file="BaseRepository.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DataMapper.SqlServerDAO
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using DataLayer.AccessLayer;

    /// <summary>
    /// Defines the <see cref="BaseRepository{T}" />.
    /// </summary>
    /// <typeparam name="T">The element type of the abstract class.</typeparam>
    public abstract class BaseRepository<T> : IRepository<T>
        where T : class
    {
        /// <summary>
        /// Applies a filter and a query and returns the resulting list of items.
        /// </summary>
        /// <param name="filter">The filter<see cref="Expression{Func{T, bool}}"/>.</param>
        /// <param name="orderBy">The orderBy<see cref="Func{IQueryable{T}, IOrderedQueryable{T}}"/>.</param>
        /// <param name="includeProperties">The includeProperties<see cref="string"/>.</param>
        /// <returns>The result of the query.</returns>
        public virtual IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            using (var ctx = new AuctionDBContext())
            {
                var dbSet = ctx.Set<T>();

                IQueryable<T> query = dbSet;

                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (orderBy != null)
                {
                    return orderBy(query).ToList();
                }
                else
                {
                    return query.ToList();
                }
            }
        }

        /// <summary>
        /// The Insert.
        /// </summary>
        /// <param name="entity">The entity<see cref="T"/>.</param>
        public virtual void Insert(T entity)
        {
            using (var ctx = new AuctionDBContext())
            {
                var dbSet = ctx.Set<T>();
                dbSet.Add(entity);

                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="item">The item<see cref="T"/>.</param>
        public virtual void Update(T item)
        {
            using (var ctx = new AuctionDBContext())
            {
                var dbSet = ctx.Set<T>();
                dbSet.Attach(item);
                ctx.Entry(item).State = EntityState.Modified;

                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="id">The id<see cref="object"/>.</param>
        public virtual void Delete(object id)
        {
            this.Delete(this.GetByID(id));
        }

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="entityToDelete">The entityToDelete<see cref="T"/>.</param>
        public virtual void Delete(T entityToDelete)
        {
            using (var ctx = new AuctionDBContext())
            {
                var dbSet = ctx.Set<T>();

                if (ctx.Entry(entityToDelete).State == EntityState.Detached)
                {
                    dbSet.Attach(entityToDelete);
                }

                dbSet.Remove(entityToDelete);

                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// Retrieves the entity identified by the id.
        /// </summary>
        /// <param name="id">The id<see cref="object"/>.</param>
        /// <returns>The object identified by the id.</returns>
        public virtual T GetByID(object id)
        {
            using (var ctx = new AuctionDBContext())
            {
                return ctx.Set<T>().Find(id);
            }
        }
    }
}
