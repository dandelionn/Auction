//-----------------------------------------------------------------------
// <copyright file="ProductRepository.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DataMapper.SqlServerDAO
{
    using DomainModel;

    /// <summary>
    /// Defines the <see cref="ProductRepository" />.
    /// </summary>
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
    }
}
