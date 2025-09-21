using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Sale
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public string Status { get; set; } = null!;
        public DateTime Expiration { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
public record SalePatchRequest(string? Status,DateTime? Expiration);
public record SaleRequest(string Status,Guid ProductId,Guid UserId,DateTime Expiration);
