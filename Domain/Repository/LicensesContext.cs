using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repository
{
    public class LicensesContext:DbContext
    {
        public LicensesContext(DbContextOptions<LicensesContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<License> Licenses { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Expert> Experts { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Sale>().ToTable("Sales");
            modelBuilder.Entity<License>().ToTable("Licenses");
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Expert>().ToTable("Experts");
        }
    }
}
