using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public List<User> ReadAllUsers()
        {
            return _userRepo.GetAllUsers().ToList();
        }

        public User UpdateUser(User userUpdate)
        {
            return _userRepo.UpdateUser(userUpdate);
        }

        public User FindUserById(int id)
        {
            return _userRepo.ReadUserById(id);
        }

        public User DeleteUser(int id)
        {
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
            return _userRepo.CreateUser(user);
        }
    }
}
