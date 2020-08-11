//-----------------------------------------------------------------------
// <copyright file="UserProfileServices.cs" company="Transilvania University of Brasov">    
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

    public class UserProfileServices
    {
        private IUserProfileRepository userProfileRepository;

        public UserProfileServices(IUserProfileRepository userProfileRepository)
        {
            this.userProfileRepository = userProfileRepository;
        }

        public void Delete(UserProfile entity)
        {
            userProfileRepository.Delete(entity);
        }

        public IEnumerable<UserProfile> GetAll()
        {
            return userProfileRepository.Get().OrderBy(userProfile => userProfile.Email).ToList();
        }

        public UserProfile GetByID(object id)
        {
            return userProfileRepository.GetByID(id);
        }

        public IList<ValidationResult> Insert(UserProfile entity)
        {
            var results = EntityValidator.IsEntityValid(entity);
            if (results.Count == 0)
            {
                userProfileRepository.Insert(entity);
            }

            return results;
        }

        public IList<ValidationResult> Update(UserProfile entity)
        {
            var results = EntityValidator.IsEntityValid(entity);
            {
                userProfileRepository.Update(entity);
            }

            return results;
        }
    }
}
