using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;
using PerformanceReport.Models;
using PerformanceReport.Repository;

namespace PerformanceReport.Repositories
{
    public class PerformanceService
    {
        private readonly ITradeRepository _repo;

        public PerformanceService(ITradeRepository repo)
        {
            _repo = repo;
        }

        public async Task<Report> GetPerformanceReportFromExpert(int expertId)
        {
            var trades = await GetTradesFromExpert(expertId);
            return new Report();
        }

        private async Task<List<Trade>> GetTradesFromExpert(int expertId)
        {
            var trades = await _repo.GetAllAsync();
            return trades.Where(t => t.Magic == expertId).ToList();
        }
    }
}
