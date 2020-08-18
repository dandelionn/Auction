//-----------------------------------------------------------------------
// <copyright file="PersonRepository.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DataMapper.SqlServerDAO
{
    using DomainModel;
    using System.Configuration;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="PersonRepository" />.
    /// </summary>
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
    }
}
