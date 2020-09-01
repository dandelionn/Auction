//-----------------------------------------------------------------------
// <copyright file="PersonService.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace ServiceLayer.ServiceImplementation
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Configuration;
    using System.Linq;
    using DataMapper;
    using DomainModel;
    using Log;

    /// <summary>
    /// Defines the <see cref="PersonService" />.
    /// </summary>
    public class PersonService
    {
        /// <summary>
        /// Defines the Logger.
        /// </summary>
        private static readonly Log4NetWrapper Logger = new Log4NetWrapper(typeof(PersonService));

        /// <summary>
        /// Defines the userRepository.
        /// </summary>
        private IPersonRepository userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonService"/> class.
        /// </summary>
        /// <param name="userRepository">The userRepository<see cref="IPersonRepository"/>.</param>
        public PersonService(IPersonRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="entity">The entity<see cref="Person"/>.</param>
        public void Delete(Person entity)
        {
            this.userRepository.Delete(entity);
        }

        /// <summary>
        /// The GetAll.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Person}"/>.</returns>
        public IEnumerable<Person> GetAll()
        {
            return this.userRepository.Get().OrderBy(user => user.Name).ToList();
        }

        /// <summary>
        /// The GetByID.
        /// </summary>
        /// <param name="id">The id<see cref="object"/>.</param>
        /// <returns>The <see cref="Person"/>.</returns>
        public Person GetByID(object id)
        {
            return this.userRepository.GetByID(id);
        }

        /// <summary>
        /// The Insert.
        /// </summary>
        /// <param name="entity">The entity<see cref="Person"/>.</param>
        /// <returns>The <see cref="IList{ValidationResult}"/>.</returns>
        public IList<ValidationResult> Insert(Person entity)
        {
            var results = EntityValidator.IsEntityValid(entity);
            if (results.Count == 0)
            {
                this.userRepository.Insert(entity);
            }
            else
            {
                Logger.Info("Person insertion failed!");
            }

            return results;
        }

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="entity">The entity<see cref="Person"/>.</param>
        /// <returns>The <see cref="IList{ValidationResult}"/>.</returns>
        public IList<ValidationResult> Update(Person entity)
        {
            CheckPersonScore(entity);
            var results = EntityValidator.IsEntityValid(entity);
            if (results.Count == 0)
            {
                this.userRepository.Update(entity);
            }
            else
            {
                Logger.Info("Person update failed");
            }

            return results;
        }

        /// <summary>
        /// The computeAverageScore.
        /// </summary>
        /// <param name="person">The person<see cref="Person"/>.</param>
        /// <returns>The <see cref="double"/>.</returns>
        private static double ComputeAverageScore(Person person)
        {
            var averageScore = double.Parse(ConfigurationManager.AppSettings.Get("InitialPersonScore"));
            var maxElements = int.Parse(ConfigurationManager.AppSettings.Get("NumOfLastScoresForAverage"));
            IList<int> scores = person.Scores;
            if (scores.Any())
            {
                var nr = 0;
                double sum = 0;
                for (int i = scores.Count - 1; i >= 0 && nr < maxElements; --i)
                {
                    sum += scores[i];
                    nr++;
                }

                averageScore = sum / nr;
            }

            return averageScore;
        }

        /// <summary>
        /// The CheckPersonScore.
        /// </summary>
        /// <param name="person">The person<see cref="Person"/>.</param>
        private static void CheckPersonScore(Person person)
        {
            var averageScore = ComputeAverageScore(person);
            var minAllowedScore = double.Parse(ConfigurationManager.AppSettings.Get("MinimumAllowedPersonScore"));
            if (averageScore < minAllowedScore)
            {
                var banDays = int.Parse(ConfigurationManager.AppSettings.Get("NumOfAuctionBanDays"));
                person.BanEndDate.Date.AddDays(banDays);
                person.Scores.Clear();
            }
        }
    }
}
