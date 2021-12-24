using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Internal;
using MyNeighbors.Core.DomainServices;
using MyNeighbors.Core.Entity;

namespace MyNeighbors.Core.ApplicationServices.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository<User> _userRepo;

        public UserService(IUserRepository<User> userRepository)
        {
            _userRepo = userRepository;
        }
        public List<User> ReadAllUsers(Filter filter)
        {
            if (filter == null || filter.ItemsPrPage == 0 && filter.CurrentPage == 0)
            {
                return _userRepo.GetAllUsers().ToList();
            }
            return _userRepo.GetAllUsers(filter).ToList();
        }

        public User UpdateUser(User userUpdate)
        {
            if (userUpdate == null)
            {
                throw new ArgumentException("User is missing");
            }
            if (!IsValidUser(userUpdate))
            {
                throw new ArgumentException("Invalid user property");
            }

            if (_userRepo.ReadUserById(userUpdate.Id) == null)
            {
                throw new InvalidOperationException("User does not exist");
            }

            return _userRepo.UpdateUser(userUpdate);
        }

        public User FindUserById(int id)
        {
            return _userRepo.ReadUserById(id);
        }

        public User DeleteUser(int id)
        {
            if (_userRepo.ReadUserById(id) == null)
            {
                throw new InvalidOperationException("Cannot remove user that does not exist");
            }
            return _userRepo.DeleteUser(id);
        }

        public User NewUser(string username, string password)
        {
            var user = new User()
            {
                Username = username,
                Password = password
            };
            return user;
        }

        public User CreateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentException("User is missing");
            }
            if (!IsValidUser(user))
            {
                throw new ArgumentException("Invalid user property");
            }
            if (_userRepo.ReadUserById(user.Id) != null)
            {
                throw new InvalidOperationException("This User already exists");
            }

            if (_userRepo.ReadUserByUsername(user.Username) != null)
            {
                throw new InvalidOperationException("This User already exists");
            }
            return _userRepo.CreateUser(user);
        }

        private bool IsValidUser(User user)
        {
            return (!user.Username.IsNullOrEmpty());
        }

        public int GetUserCount()
        {
            return _userRepo.GetUserCount();
        }
    }
}
