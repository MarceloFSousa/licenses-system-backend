
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;

namespace Domain.Repository
{
    public class LicensesContextFactory : IDesignTimeDbContextFactory<LicensesContext>
    {
        public LicensesContext CreateDbContext(string[] args)
        {
            Env.Load();

            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__LicensesDatabase");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("ConnectionStrings__LicensesDatabase env var is not set.");
            }

            var optionsBuilder = new DbContextOptionsBuilder<LicensesContext>();
            optionsBuilder.UseNpgsql(connectionString);
            return new LicensesContext(optionsBuilder.Options);
        }
    }
}