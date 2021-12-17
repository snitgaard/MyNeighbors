using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeighbors.Core.ApplicationServices;
using MyNeighbors.Core.Entity;

namespace MyNeighbors.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<List<User>> Get([FromQuery] Filter filter)
        {
            try
            {
                return _userService.ReadAllUsers(filter);
            }
            catch (Exception)
            {
                return StatusCode(500, "Could not get users");
            }
        }
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            var user = _userService.FindUserById(id);
            if (user == null)
            {
                return StatusCode(404, "User does not exist");
            }

            try
            {
                return _userService.FindUserById(id);
            }
            catch (Exception)
            {
                return StatusCode(500, "Could not return user");
            }
        }

        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            if (string.IsNullOrEmpty(user.Username))
            {
                return StatusCode(500, "Name is required for creating a user");
            }
            try
            {
                return _userService.CreateUser(user);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<User> Put(int id, [FromBody] User user)
        {
            try
            {
                return _userService.UpdateUser(user);
            }
            catch (Exception)
            {
                return StatusCode(500, "Could not update user");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<User> Delete(int id)
        {
            var user = _userService.DeleteUser(id);
            if (user == null)
            {
                return StatusCode(404, "The user you are trying to delete does not exist");
            }
            try
            {
                return user;
            }
            catch (Exception)
            {
                return StatusCode(500, "Could not delete user");
            }
        }

    }
}
