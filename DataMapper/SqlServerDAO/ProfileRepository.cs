//-----------------------------------------------------------------------
// <copyright file="UserProfileRepository.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DataMapper.SqlServerDAO
{
    using DomainModel;

    /// <summary>
    /// Defines the <see cref="UserProfileRepository" />.
    /// </summary>
    public class UserProfileRepository : BaseRepository<UserProfile>, IUserProfileRepository
    {
    }
}
