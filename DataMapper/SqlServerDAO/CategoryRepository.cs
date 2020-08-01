//-----------------------------------------------------------------------
// <copyright file="CategoryRepository.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DataMapper.SqlServerDAO
{
    using DomainModel;

    /// <summary>
    /// Defines the <see cref="CategoryRepository" />.
    /// </summary>
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
    }
}
