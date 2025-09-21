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
        Task<Sale?> UpdateAsync(SalePatchRequest sale, Guid id);
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
                .ToListAsync();
        }

        public async Task<Sale?> GetByIdAsync(Guid id)
        {
            return await _context.Sales
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Sale> AddAsync(Sale sale)
        {
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();
            return sale;
        }

        public async Task<Sale?> UpdateAsync(SalePatchRequest sale, Guid id)
{
    var existing = await _context.Sales.FindAsync(id);
    if (existing == null) return null;

    if (sale.Status != null)
        existing.Status = sale.Status;

    if (sale.Expiration != null)
        existing.Expiration = (DateTime)sale.Expiration;

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
