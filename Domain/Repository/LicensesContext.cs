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
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Sale>().ToTable("sales");
            modelBuilder.Entity<License>().ToTable("licenses");
            modelBuilder.Entity<Product>().ToTable("products");
            modelBuilder.Entity<Expert>().ToTable("experts");
        }
    }
}
