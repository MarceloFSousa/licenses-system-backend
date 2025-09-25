using Microsoft.EntityFrameworkCore;
using PerformanceReport.Models;

namespace PerformanceReport.Repository
{
    public interface ITradeRepository
    {
        Task<IEnumerable<Trade>> GetAllAsync();
        Task<List<Trade>> AddAsync(List<Trade> trade);
        Task<Trade> AddAsync(Trade trade);
    }
    public class TradeRepository:ITradeRepository
    {
        private readonly TradeContext _context;

        public TradeRepository(TradeContext context)
        {
            _context = context;
        }

        public async Task<Trade> AddAsync(Trade trade)
        {
            _context.Trades.Add(trade);
            await _context.SaveChangesAsync();
            return trade;
        }
        
        public async Task<List<Trade>> AddAsync(List<Trade> trades)
        {
            var existingTickets = await _context.Trades
                .Select(t => t.Ticket)
                .ToListAsync();

            var newTrades = trades
                .Where(t => !existingTickets.Contains(t.Ticket))
                .ToList();

            if (!newTrades.Any())
                return newTrades;

            await _context.Trades.AddRangeAsync(newTrades);
            await _context.SaveChangesAsync();

            return newTrades;
        }

        public async Task<IEnumerable<Trade>> GetAllAsync()
        {
            return await _context.Trades.ToListAsync();
        }
    }
}
