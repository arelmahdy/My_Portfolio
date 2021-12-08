using Core.EntityBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<owner>().Property(x => x.ID).HasDefaultValueSql("NewID()");
            modelBuilder.Entity<ProtiflioItem>().Property(x => x.ID).HasDefaultValueSql("NewID()");
            modelBuilder.Entity<owner>().HasData(new owner()
            {
                ID = Guid.NewGuid(),FullName = "Ahmed Raafat",Avatar = "Avatar.npg",Profile=".Net Developer"
            }); 
        }
        public DbSet<owner> Owner { get; set; }
        public DbSet<ProtiflioItem> PortfolioItems { get; set; }
    }
}
