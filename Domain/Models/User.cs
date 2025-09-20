using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
        public ICollection<License> Licenses { get; set; } = new List<License>();
    }
}
public record LoginRequest(string Email, string Password);
public record RegisterRequest(string Email, string Password,string Name,string Role);
public record UserPutRequest(string? Email, string? Name,string? Role);
