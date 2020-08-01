//-----------------------------------------------------------------------
// <copyright file="UserRepository.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DataMapper.SqlServerDAO
{
    using DomainModel;

    /// <summary>
    /// Defines the <see cref="UserRepository" />.
    /// </summary>
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
    }
}
