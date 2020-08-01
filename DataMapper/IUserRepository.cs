//-----------------------------------------------------------------------
// <copyright file="IUserRepository.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DataMapper
{
    using DomainModel;

    /// <summary>
    /// Defines the <see cref="IUserRepository" />.
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {
    }
}
