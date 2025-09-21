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

        public async Task<User?> AuthenticateAsync(User user, string password)
        {

            if (user == null)
                return null;

            // Verify hashed password
            bool isValid = BCrypt.Net.BCrypt.Verify(password, user.Password);
            return isValid ? user : null;
        }
    }
}
