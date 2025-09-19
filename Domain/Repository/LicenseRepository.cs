using Domain.Models;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ILicenseRepository
    {
        Task<IEnumerable<License>> GetAllAsync();
        Task<License?> GetByIdAsync(Guid id);
        Task<License> AddAsync(License license);
        Task<License?> UpdateAsync(License license);
        Task<bool> DeleteAsync(Guid id);
    }
    public class LicenseRepository : ILicenseRepository
    {
        private readonly LicensesContext _context;

        public LicenseRepository(LicensesContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<License>> GetAllAsync()
        {
            return await _context.Licenses
                .Include(l => l.User)     
                .Include(l => l.Product) 
                .ToListAsync();
        }

        public async Task<License?> GetByIdAsync(Guid id)
        {
            return await _context.Licenses
                .Include(l => l.User)
                .Include(l => l.Product)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<License> AddAsync(License license)
        {
            _context.Licenses.Add(license);
            await _context.SaveChangesAsync();
            return license;
        }

        public async Task<License?> UpdateAsync(License license)
        {
            var existing = await _context.Licenses.FindAsync(license.Id);
            if (existing == null) return null;

            existing.Status = license.Status;
            existing.UserId = license.UserId;
            existing.ProductId = license.ProductId;
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var license = await _context.Licenses.FindAsync(id);
            if (license == null) return false;

            _context.Licenses.Remove(license);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
