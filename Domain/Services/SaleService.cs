using Domain.Models;
using Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class SaleService
    {
        private readonly ISaleRepository _repository;

        public SaleService(ISaleRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Sale>> GetAllAsync() => _repository.GetAllAsync();

        public Task<Sale?> GetByIdAsync(Guid id) => _repository.GetByIdAsync(id);

        public Task<Sale> CreateAsync(Sale sale) => _repository.AddAsync(sale);

        public Task<Sale?> UpdateAsync(Sale sale) => _repository.UpdateAsync(sale);

        public Task<bool> DeleteAsync(Guid id) => _repository.DeleteAsync(id);
    }
}
