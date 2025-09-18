namespace Domain.Models
{
    public class License
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public User User { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
