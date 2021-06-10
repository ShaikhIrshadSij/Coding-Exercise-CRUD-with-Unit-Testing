using CodingExercise.Entity;
using CodingExercise.Entity.POCO;
using CodingExercise.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingExercise.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddUser(UsersEntity usersEntity)
        {
            await _context.UsersEntities.AddAsync(usersEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(int userId)
        {
            var isUserExist = await _context.UsersEntities.FirstOrDefaultAsync(x => x.Id == userId);
            if (isUserExist != null)
            {
                _context.UsersEntities.Remove(isUserExist);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<UsersEntity> GetUserById(int userId) => await _context.UsersEntities.FirstOrDefaultAsync(x => x.Id == userId);

        public async Task<List<UsersEntity>> GetUsersList() => await _context.UsersEntities.OrderBy(x => x.LastName).ToListAsync();

        public async Task UpdateUser(UsersEntity usersEntity)
        {
            _context.Entry(usersEntity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
