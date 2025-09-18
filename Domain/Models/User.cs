namespace Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
        public ICollection<License> Licenses { get; set; } = new List<License>();
    }
}
