﻿//-----------------------------------------------------------------------
// <copyright file="IProductRepository.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DataMapper
{
    using DomainModel;

    /// <summary>
    /// Defines the <see cref="IProductRepository" />.
    /// </summary>
    public interface IProductRepository : IRepository<Product>
    {
    }
}
