//-----------------------------------------------------------------------
// <copyright file="IPersonService.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ServiceLayer
{
    using DomainModel;

    /// <summary>
    /// Defines the <see cref="IUserService" />.
    /// </summary>
    interface IUserService : IService<User>
    {
    }
}
