using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class License
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public string Status { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
public record LicensePatchRequest(string Status);
