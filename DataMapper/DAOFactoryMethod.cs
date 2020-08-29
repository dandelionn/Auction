//-----------------------------------------------------------------------
// <copyright file="DAOFactoryMethod.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DataMapper
{
    using System.Configuration;
    using DataMapper.SqlServerDAO;

    /// <summary>
    /// Defines the <see cref="DAOFactoryMethod" />.
    /// </summary>
    public static class DAOFactoryMethod
    {
        /// <summary>
        /// Defines the currentDAOFactory.
        /// </summary>
        private static readonly IDAOFactory currentDAOFactory;

        /// <summary>
        /// Initializes static members of the <see cref="DAOFactoryMethod"/> class.
        /// </summary>
        static DAOFactoryMethod()
        {
            var currentDataProvider = ConfigurationManager.AppSettings["dataProvider"];
            if (string.IsNullOrWhiteSpace(currentDataProvider))
            {
                currentDAOFactory = null;
            }
            else
            {
                switch (currentDataProvider.ToLower().Trim())
                {
                    case "sqlserver":
                        currentDAOFactory = new SQLServerDAOFactory();
                        break;
                    case "oracle":
                        currentDAOFactory = null;
                        return;
                    default:
                        currentDAOFactory = new SQLServerDAOFactory();
                        break;
                }
            }
        }

        /// <summary>
        /// Gets the CurrentDAOFactory.
        /// </summary>
        public static IDAOFactory CurrentDAOFactory
        {
            get
            {
                return currentDAOFactory;
            }
        }
    }
}
