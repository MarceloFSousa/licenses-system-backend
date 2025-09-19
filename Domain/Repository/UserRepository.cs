using Domain.Models;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(Guid id);
        Task<User> AddAsync(User user);
        Task<User?> UpdateAsync(User user);
        Task<bool> DeleteAsync(Guid id);
    }
    public class UserRepository : IUserRepository
    {
        private readonly LicensesContext _context;

        public UserRepository(LicensesContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(u => u.Sales)
                    .ThenInclude(s => s.Product)   // include Product in Sales
                        .ThenInclude(p => p.Expert) // include Expert of Product
                .Include(u => u.Licenses)
                    .ThenInclude(l => l.Product)   // include Product in Licenses
                        .ThenInclude(p => p.Expert) // include Expert of Product
                .ToListAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users
                .Include(u => u.Sales)
                    .ThenInclude(s => s.Product)
                        .ThenInclude(p => p.Expert)
                .Include(u => u.Licenses)
                    .ThenInclude(l => l.Product)
                        .ThenInclude(p => p.Expert)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> UpdateAsync(User user)
        {
            var existing = await _context.Users.FindAsync(user.Id);
            if (existing == null) return null;

            existing.Name = user.Name;
            existing.Email = user.Email;
            existing.Password = user.Password;
            existing.Role = user.Role;
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
