//-----------------------------------------------------------------------
// <copyright file="IUserProfileRepository.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DataMapper
{
    using DomainModel;

    /// <summary>
    /// Defines the <see cref="IUserProfileRepository" />.
    /// </summary>
    public interface IUserProfileRepository : IRepository<UserProfile>
    {
    }
}
