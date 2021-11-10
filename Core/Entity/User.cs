﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeighbors.Core.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
