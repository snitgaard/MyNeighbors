using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyNeighbors.Core.DomainServices;
using MyNeighbors.Core.Entity;
using MyNeighbors.Infrastructure.Helpers;

namespace MyNeighbors.Controllers
{
    public class TokenController : ApiController
    {
        private IUserRepository<User> repository;
        private IAuthentication authentication;

        public TokenController(IUserRepository<User> repo, IAuthentication auth)
        {
            repository = repo;
            authentication = auth;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginInputModel model)
        {
            var user = repository.GetAllUsers().FirstOrDefault(u => u.Username == model.Username);
            if (user == null)
            {
                return Unauthorized("User doesnt exist. Please try again");
            }

            if (!authentication.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
            {
                return Unauthorized("Wrong password. Please try again");
            }

            return Ok(new
            {
                username = user.Username,
                token = authentication.GenerateToken(user),
                id = user.Id
            });
        }
    }
}
