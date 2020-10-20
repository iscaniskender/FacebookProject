using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;

namespace DAL
{
    public class FacebookContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-P4JO1BR\SQLEXPRESS;Database=FacebookProject;integrated security=true");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Connection>().HasKey(c => new { c.UserIdOne, c.UserIdTwo });


            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> comments { get; set; }
        public DbSet<Connection> connections { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<Share> Shares { get; set; }
    }
}
