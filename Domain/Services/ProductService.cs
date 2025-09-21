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

        public Task<Product> CreateAsync(ProductRequest product)
        {
            var newProduct = new Product { ExpertId=product.ExpertId,MaxVolume=product.MaxVolume,Name=product.Name,Price=product.Price};
            return _repository.AddAsync(newProduct);
        }

        public Task<Product?> UpdateAsync(ProductPutRequest product, Guid id) => _repository.UpdateAsync(product, id);

        public Task<bool> DeleteAsync(Guid id) => _repository.DeleteAsync(id);
    }
}
