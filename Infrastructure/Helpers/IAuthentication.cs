using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing.Tree;
using MyNeighbors.Core.Entity;

namespace MyNeighbors.Infrastructure.Helpers
{
    public interface IAuthentication
    {
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);
        public string GenerateToken(User user);
    }
}
