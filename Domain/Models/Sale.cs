namespace Domain.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; } = null!;
        public DateTime Expiration { get; set; }
        public DateTime CreatedAt { get; set; }
        public User User { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
