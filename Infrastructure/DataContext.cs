using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Owner>().Property(o => o.ID).HasDefaultValueSql("NEWID()");//to make the Guid act as identity in the sql table
            modelBuilder.Entity<PortofolioItem>().Property(o => o.ID).HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Owner>().HasData(
                new Owner
                {
                    ID = Guid.NewGuid(),
                    FullName = "Mokhtar ali",
                    Avatar = "avatar.jpg",
                    Profile = "So Called .Net Developer"
                }
                );
        }


        public DbSet<Owner> Owners { get; set; }
        public DbSet<PortofolioItem> PortofolioItems { get; set; }
    }
}
