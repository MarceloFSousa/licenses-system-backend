using Domain.Models;
using Domain.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ExpertsService
    {
        private readonly IExpertRepository _repository;

        public ExpertsService(IExpertRepository repository,LocalBucketService bucket)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Expert>> GetAllAsync() => _repository.GetAllAsync();

        public Task<Expert?> GetByIdAsync(Guid id) => _repository.GetByIdAsync(id);

        public Task<Expert> CreateAsync(string name, string description, DateTime initDate, string? imgUrl)
        {
            var newExpert = new Expert{ Description = description, InitDate = initDate, Name = name, ImgUrl=imgUrl };
            return _repository.AddAsync(newExpert);
        }

        public Task<Expert?> UpdateAsync(Expert expert) => _repository.UpdateAsync(expert);

        public Task<bool> DeleteAsync(Guid id) => _repository.DeleteAsync(id);
    }
}
