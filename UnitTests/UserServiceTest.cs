using Moq;
using MyNeighbors.Core.ApplicationServices.Services;
using MyNeighbors.Core.DomainServices;
using MyNeighbors.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTest
{
    public class UserServiceTest
    {
        private Mock<IUserRepository<User>> repoMock;

        public UserServiceTest()
        {
            repoMock = new Mock<IUserRepository<User>>();
        }

        [Fact]
        public void CreateUserService()
        {
            IUserRepository<User> repo = repoMock.Object;

            UserService service = new UserService(repo);

            Assert.NotNull(service);
        }

        [Theory]
        [InlineData(1, "Username", "Password")]
        public void AddValidUserNotExist(int id,  string username, string password)
        {
            IUserRepository<User> repo = repoMock.Object;
            UserService service = new UserService(repo);

            User u = new User()
            {
                Id = id,
                Username = username,
                Password = password
            };

            service.CreateUser(u);

            repoMock.Verify(repo => repo.CreateUser(It.Is<User>((us => us == u))), Times.Once);
        }

        [Fact]
        public void AddUserExistingUserExpectInvalidOperationException()
        {
            User u = new User()
            {
                Id = 1,
                Username = "Username",
                Password = "Password"
            };

            repoMock.Setup(repo => repo.ReadUserById(It.Is<int>(x => x == u.Id))).Returns(() => u);

            UserService service = new UserService(repoMock.Object);

            var ex = Assert.Throws<InvalidOperationException>(() => service.CreateUser(u));

            Assert.Equal("User already exists", ex.Message);
            repoMock.Verify(repo => repo.CreateUser(It.Is<User>(us => us == u)), Times.Never);
        }

        [Fact]
        public void AddUserIsNullExpectArgumentException()
        {
            UserService service = new UserService(repoMock.Object);

            var ex = Assert.Throws<ArgumentException>(() => service.CreateUser(null));

            Assert.Equal("User is missing", ex.Message);
            repoMock.Verify(repo => repo.CreateUser(It.Is<User>(u => u == null)), Times.Never);
        }

        [Theory]
        [InlineData(1, null, "Password")] //Testing for missing username
        [InlineData(1, "", "Password")] //Testing for empty username
        public void AddInvalidUserExpectArgumentException(int id, string username, string password)
        {
            UserService service = new UserService(repoMock.Object);

            User u = new User()
            {
                Id = id,
                Username = username,
                Password = password,
            };

            var ex = Assert.Throws<ArgumentException>(() => service.CreateUser(u));

            Assert.Equal("Invalid user property", ex.Message);
            repoMock.Verify(repo => repo.CreateUser((It.Is<User>(us => us == u))), Times.Never);
        }

        [Fact]
        public void UpdateValidUser()
        {
            User u = new User()
            {
                Id = 1,
                Username = "Username",
                Password = "Password"
            };

            repoMock.Setup(repo => repo.ReadUserById(It.Is<int>(x => x == u.Id))).Returns(() => u);

            UserService service = new UserService(repoMock.Object);

            service.UpdateUser(u);

            repoMock.Verify(repo => repo.UpdateUser(It.Is<User>((us => us == u))), Times.Once);
        }

        [Fact]
        public void UpdateUserIsNullExpectArgumentException()
        {
            UserService service = new UserService(repoMock.Object);

            var ex = Assert.Throws<ArgumentException>(() => service.UpdateUser(null));

            Assert.Equal("User is missing", ex.Message);
            repoMock.Verify(repo => repo.UpdateUser(It.Is<User>(u => u == null)), Times.Never);
        }

        [Fact]
        public void UpdateUserNotExistingExpectInvalidOperationException()
        {
            User u = new User()
            {
                Id = 1,
                Username = "Username",
                Password = "Password"
            };

            repoMock.Setup(repo => repo.ReadUserById(It.Is<int>(x => x == u.Id))).Returns(() => null);

            UserService service = new UserService(repoMock.Object);

            var ex = Assert.Throws<InvalidOperationException>(() => service.UpdateUser(u));

            Assert.Equal("User does not exist", ex.Message);
            repoMock.Verify(repo => repo.CreateUser(It.Is<User>(us => us == u)), Times.Never);
        }

        [Theory]
        [InlineData(1, null, "Password")] //Testing for missing username
        [InlineData(1, "", "Password")] //Testing for empty username
        public void UpdateUserInvalidPropertyExpectArgumentException(int id, string username, string password)
        {
            UserService service = new UserService(repoMock.Object);

            User u = new User()
            {
                Id = id,
                Username = username,
                Password = password,
            };

            var ex = Assert.Throws<ArgumentException>(() => service.UpdateUser(u));

            Assert.Equal("Invalid user property", ex.Message);
            repoMock.Verify(repo => repo.UpdateUser((It.Is<User>(us => us == u))), Times.Never);
        }

        [Fact]
        public void DeleteExistingUser()
        {
            User u = new User()
            {
                Id = 1,
                Username = "Username",
                Password = "Password"
            };

            repoMock.Setup(repo => repo.ReadUserById(It.Is<int>(x => x == u.Id))).Returns(() => u);

            UserService service = new UserService(repoMock.Object);

            service.DeleteUser(u.Id);

            repoMock.Verify(repo => repo.DeleteUser(It.Is<int>(us => us == u.Id)), Times.Once);
        }

        [Fact]
        public void DeleteUserNotExistExpectInvalidOperationException()
        {
            User u = new User()
            {
                Id = 1,
                Username = "Username",
                Password = "Password"
            };

            repoMock.Setup(repo => repo.ReadUserById(It.Is<int>(x => x == u.Id))).Returns(() => null);

            UserService service = new UserService(repoMock.Object);

            var ex = Assert.Throws<InvalidOperationException>(() => service.DeleteUser(u.Id));

            Assert.Equal("Cannot remove user that does not exist", ex.Message);
            repoMock.Verify(repo => repo.DeleteUser(It.Is<int>(us => us == u.Id)), Times.Never);
        }

        [Fact]
        public void GetUserByIdExistingUser()
        {
            User u = new User()
            {
                Id = 1,
                Username = "Username",
                Password = "Password"
            };

            repoMock.Setup(repo => repo.ReadUserById(It.Is<int>(x => x == u.Id))).Returns(() => null);

            UserService service = new UserService(repoMock.Object);

            var result = service.FindUserById(u.Id);

            Assert.Null(result);
            repoMock.Verify(repo => repo.ReadUserById(It.Is<int>(x => x == u.Id)), Times.Once);
        }

        [Fact]
        public void GetUserByIdNonExistingUserReturnsNull()
        {
            int id = 1;

            repoMock.Setup(repo => repo.ReadUserById(It.Is<int>(x => x == id))).Returns(() => null);

            UserService service = new UserService(repoMock.Object);

            var result = service.FindUserById(id);

            Assert.Null(result);
            repoMock.Verify(repo => repo.ReadUserById(It.Is<int>(x => x == id)), Times.Once);
        }

        [Theory]        // empty repository, 1 in repository, n in repository
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void GetAllUsers(int userCount)
        {
            Filter filter = new Filter() 
            {
                ItemsPrPage = 2,
                CurrentPage = 1
            };
            // arrange
            List<User> data = new List<User>()
            {
                new User(){ Id = 1},
                new User() { Id = 2}
            };

            repoMock.Setup(repo => repo.GetAllUsers(filter)).Returns(() => data.GetRange(0, userCount));

            UserService service = new UserService(repoMock.Object);

            // act

            var result = service.ReadAllUsers(filter);

            // assert
            Assert.Equal(result.Count(), userCount);
            Assert.Equal(result.ToList(), data.GetRange(0, userCount));
            repoMock.Verify(repo => repo.GetAllUsers(filter), Times.Once);
        }

    }
}