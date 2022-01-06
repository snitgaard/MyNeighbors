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
        public List<User> GetAllUsers(Filter filter)
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

            if (_userRepo.GetUserById(userUpdate.Id) == null)
            {
                throw new InvalidOperationException("User does not exist");
            }

            return _userRepo.UpdateUser(userUpdate);
        }

        public User GetUserById(int id)
        {
            return _userRepo.GetUserById(id);
        }

        public User DeleteUser(int id)
        {
            if (_userRepo.GetUserById(id) == null)
            {
                throw new InvalidOperationException("Cannot remove user that does not exist");
            }
            return _userRepo.DeleteUser(id);
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
            if (_userRepo.GetUserById(user.Id) != null)
            {
                throw new InvalidOperationException("This User already exists");
            }

            if (_userRepo.GetUserByUsername(user.Username) != null)
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
