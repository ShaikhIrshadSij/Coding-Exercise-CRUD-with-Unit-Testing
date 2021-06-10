using CodingExercise.Entity.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingExercise.Services.Interface
{
    public interface IUserService
    {
        Task<List<UsersEntity>> GetUsersList();
        Task AddUser(UsersEntity usersEntity);
        Task UpdateUser(UsersEntity usersEntity);
        Task DeleteUser(int userId);
        Task<UsersEntity> GetUserById(int userId);
    }
}
