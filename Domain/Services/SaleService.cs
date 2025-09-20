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

        public Task<Sale?> UpdateAsync(SalePatchRequest sale, Guid id) => _repository.UpdateAsync(sale, id);

        public Task<bool> DeleteAsync(Guid id) => _repository.DeleteAsync(id);
    }
}
