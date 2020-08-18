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
    using System.Configuration;
    using System.Linq;
    using DataMapper;
    using DomainModel;

    public class PersonServices
    {
        private IPersonRepository userRepository;

        public PersonServices(IPersonRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void Delete(Person entity)
        {
            userRepository.Delete(entity);
        }

        public IEnumerable<Person> GetAll()
        {
            return userRepository.Get().OrderBy(user => user.Name).ToList();
        }

        public Person GetByID(object id)
        {
            return userRepository.GetByID(id);
        }

        public static double computeAverageScore(Person person)
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

        private static void CheckPersonScore(Person person)
        {
            var averageScore = computeAverageScore(person);
            var minAllowedScore = double.Parse(ConfigurationManager.AppSettings.Get("MinimumAllowedPersonScore"));
            if (averageScore < minAllowedScore)
            {
                var banDays = int.Parse(ConfigurationManager.AppSettings.Get("NumOfAuctionBanDays"));
                person.BanEndDate.Date.AddDays(banDays);
                person.Scores.Clear();
            }
        }

        public IList<ValidationResult> Insert(Person entity)
        {
            var results = EntityValidator.IsEntityValid(entity);
            if (results.Count == 0)
            {
                userRepository.Insert(entity);
            }

            return results;
        }

        public IList<ValidationResult> Update(Person entity)
        {
            CheckPersonScore(entity);
            var results = EntityValidator.IsEntityValid(entity);
            {
                userRepository.Update(entity);
            }

            return results;
        }
    }
}
