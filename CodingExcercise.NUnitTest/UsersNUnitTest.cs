using CodingExercise.Entity.POCO;
using CodingExercise.Services.Interface;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodingExcercise.NUnitTest
{
    public class UsersNUnitTest
    {
        private Mock<IUserService> _userService;

        [SetUp]
        public void Setup()
        {
            _userService = new Mock<IUserService>();
        }

        [Test]
        public async Task Index_GetAllUsers()
        {
            _userService.Setup(repo => repo.GetUsersList()).ReturnsAsync(new List<UsersEntity>()
            {
                 new UsersEntity()
                 {
                    FirstName = "First Name",
                    LastName = "Last Name",
                    PhoneNumber = "Phone Number",
                    City = "Surat"
                 }
            });

            var result = await _userService.Object.GetUsersList();
            Assert.True(result.Count == 1);
        }

        [Test]
        public async Task Create_Users()
        {
            UsersEntity usersEntity = null;
            _userService.Setup(r => r.AddUser(It.IsAny<UsersEntity>()))
                .Callback<UsersEntity>(x => usersEntity = x);
            var userMock = new UsersEntity
            {
                FirstName = "First Name",
                LastName = "Last Name",
                PhoneNumber = "Phone Number",
                City = "Surat"
            };
            await _userService.Object.AddUser(userMock);
            _userService.Verify(x => x.AddUser(It.IsAny<UsersEntity>()), Times.Once);

            Assert.AreSame(usersEntity.FirstName, userMock.FirstName);
            Assert.AreSame(usersEntity.LastName, userMock.LastName);
            Assert.AreSame(usersEntity.PhoneNumber, userMock.PhoneNumber);
            Assert.AreSame(usersEntity.City, userMock.City);
        }

        [Test]
        public async Task Update_Users()
        {
            UsersEntity updateUsersEntity = null;
            _userService.Setup(r => r.UpdateUser(It.IsAny<UsersEntity>()))
                .Callback<UsersEntity>(x => updateUsersEntity = x);
            var updateUserMock = new UsersEntity
            {
                FirstName = "First Name 1",
                LastName = "Last Name 1",
                PhoneNumber = "Phone Number 1",
                City = "Surat 1",
                Id = 1
            };
            await _userService.Object.UpdateUser(updateUserMock);
            _userService.Verify(x => x.UpdateUser(It.IsAny<UsersEntity>()), Times.Once);

            Assert.AreSame(updateUsersEntity.FirstName, updateUserMock.FirstName);
            Assert.AreSame(updateUsersEntity.LastName, updateUserMock.LastName);
            Assert.AreSame(updateUsersEntity.PhoneNumber, updateUserMock.PhoneNumber);
            Assert.AreSame(updateUsersEntity.City, updateUserMock.City);
        }


        [Test]
        public async Task Delete_Users()
        {
            var userId = 1;
            _userService.Setup(repo => repo.DeleteUser(userId));

            await _userService.Object.DeleteUser(userId);

            _userService.Verify(repo => repo.DeleteUser(userId), Times.Once);
        }
    }
}