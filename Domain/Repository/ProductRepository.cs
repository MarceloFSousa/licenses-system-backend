using Domain.Models;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(Guid id);
        Task<Product> AddAsync(Product product);
        Task<Product?> UpdateAsync(ProductPutRequest product, Guid id);
        Task<bool> DeleteAsync(Guid id);
    }
    public class ProductRepository : IProductRepository
    {
        private readonly LicensesContext _context;

        public ProductRepository(LicensesContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.Sales)         // include Sales
                .Include(p => p.Licenses)      // include Licenses
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _context.Products
                .Include(p => p.Sales)
                .Include(p => p.Licenses)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> UpdateAsync(ProductPutRequest product, Guid id)
{
    var existing = await _context.Products.FindAsync(id);
    if (existing == null) return null;

    if (!string.IsNullOrEmpty(product.Name))
        existing.Name = product.Name;

    if (product.Price != default) // e.g., >0 check if needed
        existing.Price = (decimal)product.Price;

    if (product.MaxVolume != default)
        existing.MaxVolume = (int)product.MaxVolume;

    await _context.SaveChangesAsync();
    return existing;
}


        public async Task<bool> DeleteAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
