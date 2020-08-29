//-----------------------------------------------------------------------
// <copyright file="IPersonRepository.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DataMapper
{
    using DomainModel;

    /// <summary>
    /// Defines the <see cref="IPersonRepository" />.
    /// </summary>
    public interface IPersonRepository : IRepository<Person>
    {
    }
}
