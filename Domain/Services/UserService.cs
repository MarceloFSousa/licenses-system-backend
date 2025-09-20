using Domain.Models;
using Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class UserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<User>> GetAllAsync() => _repository.GetAllAsync();

        public Task<User?> GetByIdAsync(Guid id) => _repository.GetByIdAsync(id);

        public Task<User> CreateAsync(RegisterRequest req, string hashedPassword)  {
            var user = new User
            {
                Name = req.Name,
                Email = req.Email,
                Password = hashedPassword,
                Role =req.Role,
            };
            return  _repository.AddAsync(user);
        }


        public Task<User?> UpdateAsync(UserPutRequest user, Guid id) { return _repository.UpdateAsync(user, id); }

        public Task<bool> DeleteAsync(Guid id) => _repository.DeleteAsync(id);
    }
}
