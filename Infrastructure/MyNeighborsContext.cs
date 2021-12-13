using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyNeighbors.Core.Entity;

namespace MyNeighbors.Infrastructure
{
    public class MyNeighborsContext : DbContext
    {
        public MyNeighborsContext(DbContextOptions<MyNeighborsContext> opt) : base(opt)
        {
        }

        public void DetachAll()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                entry.State = EntityState.Detached;
            }
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Sponsor> Sponsor { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>().HasKey(r => new
            {
                r.Id
            });
            modelBuilder.Entity<Address>().HasNoKey();
            modelBuilder.Entity<User>().HasKey(u => new
            {
                u.Id
            });
            modelBuilder.Entity<User>().Ignore(u => u.Password);
        }
    }
}
