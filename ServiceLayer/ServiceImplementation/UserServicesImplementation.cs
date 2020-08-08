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

    public class UserServicesImplementation
    {
        private IUserRepository userRepository;

        public UserServicesImplementation(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void Delete(User entity)
        {
            userRepository.Delete(entity);
        }

        public IEnumerable<User> GetAll()
        {
            return userRepository.Get().OrderBy(user => user.Name).ToList();
        }

        public User GetByID(object id)
        {
            return userRepository.GetByID(id);
        }

        public IList<ValidationResult> Insert(User entity)
        {
            var results = EntityValidator.IsEntityValid(entity);
            if (results.Count == 0)
            {
                userRepository.Insert(entity);
            }

            return results;
        }

        public IList<ValidationResult> Update(User entity)
        {
            var results = EntityValidator.IsEntityValid(entity);
            {
                userRepository.Update(entity);
            }

            return results;
        }
    }
}
