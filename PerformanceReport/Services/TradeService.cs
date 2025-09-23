using PerformanceReport.Models;
using PerformanceReport.Repository;

namespace PerformanceReport.Services
{
    public class TradeService
    {
        private readonly ITradeRepository _repo;

        public TradeService(ITradeRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Trade>> SaveTradeAsync(TradeRequest trade)
        {
            await _repo.AddAsync(trade.Trades);
            return trade.Trades;
        }

        public async Task<List<Trade>> GetAllTrades()
        {
            return (await _repo.GetAllAsync()).ToList();
        }
    }
}
