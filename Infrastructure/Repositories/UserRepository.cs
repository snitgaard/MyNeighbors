using MyNeighbors.Core.DomainServices;
using MyNeighbors.Core.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using MyNeighbors.Infrastructure.Helpers;

namespace MyNeighbors.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository<User>
    {
        private MyNeighborsContext _ctx;
        private readonly IAuthentication _authentication;

        public UserRepository(MyNeighborsContext ctx, IAuthentication authentication)
        {
            _ctx = ctx;
            _authentication = authentication;
        }
        public User CreateUser(User user)
        {
            byte[] passwordHash1, passwordSalt1;
            _authentication.CreatePasswordHash(user.Password, out passwordHash1, out passwordSalt1);
            user.PasswordHash = passwordHash1;
            user.PasswordSalt = passwordSalt1;
            _ctx.DetachAll();
            _ctx.Attach(user).State = EntityState.Added;
            _ctx.SaveChanges();
            return user;
        }

        public User DeleteUser(int id)
        {
            var user = _ctx.User.FirstOrDefault(u => u.Id == id);
            var userRemoved = _ctx.Remove(user).Entity;
            _ctx.SaveChanges();
            return userRemoved;
        }

        public IEnumerable<User> GetAllUsers(Filter filter)
        {
            if (filter == null)
            {
                return _ctx.User.AsNoTracking();
            }
            
            return _ctx.User.AsNoTracking().Skip((filter.CurrentPage - 1) * filter.ItemsPrPage).Take(filter.ItemsPrPage);
        }

        public User ReadUserById(int id)
        {
            return _ctx.User.AsNoTracking().FirstOrDefault(u => u.Id == id);
        }

        public User ReadUserByUsername(string username)
        {
            return _ctx.User.AsNoTracking().FirstOrDefault(u => u.Username == username);
        }

        public User UpdateUser(User updateUser)
        {
            if (!string.IsNullOrEmpty(updateUser.Password))
            {
                byte[] passwordHash1, passwordSalt1;
                _authentication.CreatePasswordHash(updateUser.Password, out passwordHash1, out passwordSalt1);
                updateUser.PasswordHash = passwordHash1;
                updateUser.PasswordSalt = passwordSalt1;
            }
            else
            {
                var userFromDB = _ctx.User.FirstOrDefault(u => u.Id == updateUser.Id);
                updateUser.PasswordHash = userFromDB.PasswordHash;
                updateUser.PasswordSalt = userFromDB.PasswordSalt;
                updateUser.IsAdmin = userFromDB.IsAdmin;
            }
            _ctx.DetachAll();
            _ctx.Attach(updateUser).State = EntityState.Modified;
            _ctx.SaveChanges();
            return updateUser;
        }
    }
}
