//-----------------------------------------------------------------------
// <copyright file="PersonServicesImplementation.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ServiceLayer.ServiceImplementation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DataMapper;
    using DomainModel;

    public class PersonServicesImplementation
    {
        private IUserRepository personRepository;

        public PersonServicesImplementation(IUserRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        public void Delete(User entity)
        {
            personRepository.Delete(entity);
        }

        public IEnumerable<User> GetAll()
        {
            return personRepository.Get().OrderBy(person => person.Name).ToList();
        }

        public User GetByID(object id)
        {
            return personRepository.GetByID(id);
        }

        public IList<ValidationResult> Insert(User entity)
        {
            var results = EntityValidator.IsEntityValid(entity);
            if (results.Count == 0)
            {
                personRepository.Insert(entity);
            }

            return results;
        }

        public IList<ValidationResult> Update(User entity)
        {
            var results = EntityValidator.IsEntityValid(entity);
            {
                personRepository.Update(entity);
            }

            return results;
        }
    }
}
