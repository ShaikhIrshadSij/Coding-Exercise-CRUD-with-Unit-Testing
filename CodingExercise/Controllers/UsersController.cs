using CodingExercise.Entity.POCO;
using CodingExercise.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingExercise.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index() => View();

        public async Task<IActionResult> GetUsersList() => Ok(await _userService.GetUsersList());
        [HttpPost]
        public async Task<IActionResult> AddUser(UsersEntity usersEntity)
        {
            await _userService.AddUser(usersEntity);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUser(UsersEntity usersEntity)
        {
            await _userService.UpdateUser(usersEntity);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            await _userService.DeleteUser(userId);
            return Ok();
        }
    }
}
