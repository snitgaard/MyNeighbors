using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyNeighbors.Core.Entity;

namespace MyNeighbors.Core.DomainServices
{
    public interface IUserRepository<T>
    {
        IEnumerable<T> GetAllUsers();
        User CreateUser(User user);
        User UpdateUser(User updateUser);
        User ReadUserById(int id);
        User ReadUserByUsername(string username);
        User DeleteUser(int id);
    }
}
