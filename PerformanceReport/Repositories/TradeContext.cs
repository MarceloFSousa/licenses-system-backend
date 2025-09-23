using Domain.Models;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using PerformanceReport.Models;

namespace PerformanceReport.Repository
{
    public class TradeContext : DbContext
    {
        public TradeContext(DbContextOptions<TradeContext> options) : base(options) { }

        public DbSet<Trade> Trades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trade>().ToTable("Trades");
        }
    }
}
