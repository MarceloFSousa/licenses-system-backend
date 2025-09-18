using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repository
{
    public class LicensesContextFactory : IDesignTimeDbContextFactory<LicensesContext>
    {
        public LicensesContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LicensesContext>();

            optionsBuilder.UseNpgsql(
                "Server=localhost;Database=LicensesDb;User Id=sa;Password=YourPassword;TrustServerCertificate=True");

            return new LicensesContext(optionsBuilder.Options);
            Env.Load();

            var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("CONNECTION_STRING env var is not set.");
            }

            var optionsBuilder = new DbContextOptionsBuilder<LicensesContext>();
            optionsBuilder.UseNpgsql(connectionString);
        }
    }