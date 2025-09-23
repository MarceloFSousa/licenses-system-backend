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
        Task<User?> GetByEmailAsync(string email);
        Task<User> AddAsync(User user);
        Task<User?> UpdateAsync(UserPutRequest user, Guid id);
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
                .Include(u => u.Licenses).AsSplitQuery() 
                .ToListAsync();
        }
public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users
                .Include(u => u.Sales)
                .Include(u => u.Licenses).ThenInclude(l=>l.Product)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> UpdateAsync(UserPutRequest user,Guid id)
{
    var existing = await _context.Users.FindAsync(id);
    if (existing == null) return null;

    if (!string.IsNullOrEmpty(user.Name))
        existing.Name = user.Name;

    if (!string.IsNullOrEmpty(user.Email))
        existing.Email = user.Email;

    if (user.Role != null)
        existing.Role = user.Role;
    if (user.AccountNumber != null)
        existing.AccountNumber =(int) user.AccountNumber;

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
