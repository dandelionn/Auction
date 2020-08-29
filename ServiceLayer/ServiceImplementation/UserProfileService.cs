//-----------------------------------------------------------------------
// <copyright file="UserProfileService.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace ServiceLayer.ServiceImplementation
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using DataLayer;
    using DataMapper;
    using DomainModel;

    /// <summary>
    /// Defines the <see cref="UserProfileService" />.
    /// </summary>
    public class UserProfileService
    {
        /// <summary>
        /// Defines the Logger.
        /// </summary>
        private static readonly Log4NetWrapper Logger = new Log4NetWrapper(typeof(UserProfileService));

        /// <summary>
        /// Defines the userProfileRepository.
        /// </summary>
        private IUserProfileRepository userProfileRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfileService"/> class.
        /// </summary>
        /// <param name="userProfileRepository">The userProfileRepository<see cref="IUserProfileRepository"/>.</param>
        public UserProfileService(IUserProfileRepository userProfileRepository)
        {
            this.userProfileRepository = userProfileRepository;
        }

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="entity">The entity<see cref="UserProfile"/>.</param>
        public void Delete(UserProfile entity)
        {
            this.userProfileRepository.Delete(entity);
        }

        /// <summary>
        /// The GetAll.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{UserProfile}"/>.</returns>
        public IEnumerable<UserProfile> GetAll()
        {
            return this.userProfileRepository.Get().OrderBy(userProfile => userProfile.Email).ToList();
        }

        /// <summary>
        /// The GetByID.
        /// </summary>
        /// <param name="id">The id<see cref="object"/>.</param>
        /// <returns>The <see cref="UserProfile"/>.</returns>
        public UserProfile GetByID(object id)
        {
            return this.userProfileRepository.GetByID(id);
        }

        /// <summary>
        /// The Insert.
        /// </summary>
        /// <param name="entity">The entity<see cref="UserProfile"/>.</param>
        /// <returns>The <see cref="IList{ValidationResult}"/>.</returns>
        public IList<ValidationResult> Insert(UserProfile entity)
        {
            var results = EntityValidator.IsEntityValid(entity);
            if (results.Count == 0)
            {
                this.userProfileRepository.Insert(entity);
            }
            else
            {
                Logger.Info("Person insertion failed");
            }

            return results;
        }

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="entity">The entity<see cref="UserProfile"/>.</param>
        /// <returns>The <see cref="IList{ValidationResult}"/>.</returns>
        public IList<ValidationResult> Update(UserProfile entity)
        {
            var results = EntityValidator.IsEntityValid(entity);
            if (results.Count == 0)
            {
                this.userProfileRepository.Update(entity);
            }
            else
            {
                Logger.Info("Person update failed");
            }

            return results;
        }
    }
}
