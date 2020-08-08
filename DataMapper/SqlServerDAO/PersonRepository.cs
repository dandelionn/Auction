//-----------------------------------------------------------------------
// <copyright file="PersonRepository.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DataMapper.SqlServerDAO
{
    using DomainModel;

    /// <summary>
    /// Defines the <see cref="PersonRepository" />.
    /// </summary>
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
    }
}
