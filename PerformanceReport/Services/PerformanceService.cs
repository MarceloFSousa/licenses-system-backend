using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;
using PerformanceReport.Models;
using PerformanceReport.Repository;
using PerformanceReport.Utils;

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
            var report = new Report
            {
                Trades = PerformanceUtil.GetNumOfTrades(trades),
                WinningTrades = PerformanceUtil.GetWinningTrades(trades),
                LosingTrades = PerformanceUtil.GetLosingTrades(trades),
                BestTrade = PerformanceUtil.GetBestTrade(trades),
                WorstTrade = PerformanceUtil.GetWorstTrade(trades),
                AverageProfit = PerformanceUtil.GetAverageProfit(trades),
                ProfitFactor = PerformanceUtil.GetProfitFactor(trades),
                Drawdown = PerformanceUtil.GetDrawdown(trades)
            };
            return report;
        }

        private async Task<List<Trade>> GetTradesFromExpert(int expertId)
        {
            var trades = await _repo.GetAllAsync();
            return trades.Where(t => t.Magic == expertId).ToList();
        }
    }
}
