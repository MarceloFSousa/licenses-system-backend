using Domain.Models;
using Domain.Repositories;
using BCrypt.Net;

namespace Domain.Services
{
    public class AuthService
    {

        public string HashesPassword(string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            return hashedPassword;
        }

        // public async Task<User?> AuthenticateAsync(string email, string password)
        // {
        //     var users = await _userRepository.GetAllAsync();
        //     var user = users.FirstOrDefault(u => u.Email == email);

        //     if (user == null)
        //         return null;

        //     // Verify hashed password
        //     bool isValid = BCrypt.Net.BCrypt.Verify(password, user.Password);
        //     return isValid ? user : null;
        // }
    }
}
