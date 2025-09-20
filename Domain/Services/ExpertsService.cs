using Domain.Models;
using Domain.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ExpertsService
    {
        private readonly IExpertRepository _repository;

        public ExpertsService(IExpertRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Expert>> GetAllAsync() => _repository.GetAllAsync();

        public Task<Expert?> GetByIdAsync(Guid id) => _repository.GetByIdAsync(id);

        public Task<Expert> CreateAsync(string name, string description, DateTime initDate, string? imgUrl, string? fileContentUrl)
        {
            var newExpert = new Expert{ Description = description, InitDate = initDate, Name = name, ImgUrl=imgUrl,FileContentUrl=fileContentUrl };
            return _repository.AddAsync(newExpert);
        }

        public Task<Expert?> UpdateAsync(ExpertPutRequest expert, Guid id)
        {
            return _repository.UpdateAsync(expert, id);
        }
        public Task<bool> DeleteAsync(Guid id) => _repository.DeleteAsync(id);
    }
}
