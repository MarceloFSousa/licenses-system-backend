using Domain.Models;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ISaleRepository
    {
        Task<IEnumerable<Sale>> GetAllAsync();
        Task<Sale?> GetByIdAsync(Guid id);
        Task<Sale> AddAsync(Sale sale);
        Task<Sale?> UpdateAsync(Sale sale);
        Task<bool> DeleteAsync(Guid id);
    }
    public class SaleRepository : ISaleRepository
    {
        private readonly LicensesContext _context;

        public SaleRepository(LicensesContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Sale>> GetAllAsync()
        {
            return await _context.Sales
                .Include(s => s.User)     // include User
                .Include(s => s.Product)  // include Product
                    .ThenInclude(p => p.Expert) // include Expert of the Product
                .ToListAsync();
        }

        public async Task<Sale?> GetByIdAsync(Guid id)
        {
            return await _context.Sales
                .Include(s => s.User)
                .Include(s => s.Product)
                    .ThenInclude(p => p.Expert)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Sale> AddAsync(Sale sale)
        {
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();
            return sale;
        }

        public async Task<Sale?> UpdateAsync(Sale sale)
        {
            var existing = await _context.Sales.FindAsync(sale.Id);
            if (existing == null) return null;

            existing.Status = sale.Status;
            existing.ProductId = sale.ProductId;
            existing.UserId = sale.UserId;
            existing.Expiration = sale.Expiration;
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale == null) return false;

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
