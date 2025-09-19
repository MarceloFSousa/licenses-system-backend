using Domain.Models;
using Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class LicenseService
    {
        private readonly ILicenseRepository _repository;

        public LicenseService(ILicenseRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<License>> GetAllAsync() => _repository.GetAllAsync();

        public Task<License?> GetByIdAsync(Guid id) => _repository.GetByIdAsync(id);

        public Task<License> CreateAsync(License license) => _repository.AddAsync(license);

        public Task<License?> UpdateAsync(License license) => _repository.UpdateAsync(license);

        public Task<bool> DeleteAsync(Guid id) => _repository.DeleteAsync(id);
    }
}
