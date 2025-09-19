using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IExpertRepository
    {
        Task<IEnumerable<Expert>> GetAllAsync();
        Task<Expert?> GetByIdAsync(Guid id);
        Task<Expert> AddAsync(Expert expert);
        Task<Expert?> UpdateAsync(Expert expert);
        Task<bool> DeleteAsync(Guid id);
    }

    public class ExpertRepository : IExpertRepository
    {
        private readonly LicensesContext _context;

        public ExpertRepository(LicensesContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Expert>> GetAllAsync()
        {
            return await _context.Experts.Include(e => e.Products).ToListAsync();
        }

        public async Task<Expert?> GetByIdAsync(Guid id)
        {
            return await _context.Experts.Include(e => e.Products)
                                         .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Expert> AddAsync(Expert expert)
        {
            _context.Experts.Add(expert);
            await _context.SaveChangesAsync();
            return expert;
        }

        public async Task<Expert?> UpdateAsync(Expert expert)
        {
            var existing = await _context.Experts.FindAsync(expert.Id);
            if (existing == null) return null;

            existing.Name = expert.Name;
            existing.Description = expert.Description;
            existing.InitDate = expert.InitDate;
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var expert = await _context.Experts.FindAsync(id);
            if (expert == null) return false;

            _context.Experts.Remove(expert);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
