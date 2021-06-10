using CodingExercise.Entity.POCO;
using CodingExercise.Services.Interface;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CodingExercise.Test
{
    public class UsersTest
    {
        private readonly Mock<IUserService> _userService;

        public UsersTest()
        {
            _userService = new Mock<IUserService>();
        }

        [Fact]
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

        [Fact]
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

            Assert.Equal(usersEntity.FirstName, userMock.FirstName);
            Assert.Equal(usersEntity.LastName, userMock.LastName);
            Assert.Equal(usersEntity.PhoneNumber, userMock.PhoneNumber);
            Assert.Equal(usersEntity.City, userMock.City);
        }

        [Fact]
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

            Assert.Equal(updateUsersEntity.FirstName, updateUserMock.FirstName);
            Assert.Equal(updateUsersEntity.LastName, updateUserMock.LastName);
            Assert.Equal(updateUsersEntity.PhoneNumber, updateUserMock.PhoneNumber);
            Assert.Equal(updateUsersEntity.City, updateUserMock.City);
        }


        [Fact]
        public async Task Delete_Users()
        {
            var userId = 1;
            _userService.Setup(repo => repo.DeleteUser(userId));

            await _userService.Object.DeleteUser(userId);

            _userService.Verify(repo => repo.DeleteUser(userId), Times.Once);
        }
    }
}
