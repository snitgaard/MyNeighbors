using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyNeighbors.Core.Entity;

namespace MyNeighbors.Core.ApplicationServices
{
    public interface IUserService
    {
        List<User> ReadAllUsers(Filter filter);
        User UpdateUser(User userUpdate);
        User FindUserById(int id);
        User DeleteUser(int id);
        User NewUser(string username, string password);
        User CreateUser(User user);
        int GetUserCount();
    }
}
