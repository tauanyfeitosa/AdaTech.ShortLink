using AdaTech.ShortLink.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AdaTech.ShortLink.DataLibrary
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Link> Links { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Link>().HasKey(l => l.Id);
        }
    }

}
