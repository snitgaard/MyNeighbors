using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyNeighbors.Core.Entity;

namespace MyNeighbors.Core.ApplicationServices
{
    public interface IUserService
    {
        List<User> GetAllUsers(Filter filter);
        User UpdateUser(User userUpdate);
        User GetUserById(int id);
        User DeleteUser(int id);
        User CreateUser(User user);
        int GetUserCount();
    }
}
