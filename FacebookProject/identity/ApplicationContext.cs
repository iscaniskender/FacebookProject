using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacebookProject.identity
{
    public class ApplicationContext:IdentityDbContext<UserTable>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Message>()
                .HasOne<UserTable>(a => a.User)
                .WithMany(d => d.Messages)
                .HasForeignKey(d => d.UserID);
        }
        public DbSet<Message> Messages { get; set; }
    }
}
