using Domain.Models;
using Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Product>> GetAllAsync() => _repository.GetAllAsync();

        public Task<Product?> GetByIdAsync(Guid id) => _repository.GetByIdAsync(id);

        public Task<Product> CreateAsync(Product product) => _repository.AddAsync(product);

        public Task<Product?> UpdateAsync(Product product) => _repository.UpdateAsync(product);

        public Task<bool> DeleteAsync(Guid id) => _repository.DeleteAsync(id);
    }
}
