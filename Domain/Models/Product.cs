using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Product
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int MaxVolume { get; set; }
        public Guid ExpertId { get; set; }
        public DateTime? CreatedAt { get; set; }

        // Relações
        public Expert? Expert { get; set; } = null!;
        public ICollection<Sale>? Sales { get; set; } = new List<Sale>();
        public ICollection<License>? Licenses { get; set; } = new List<License>();
    }
}
public record ProductPutRequest(decimal? Price, string? Name,int? MaxVolume);
public record ProductRequest(decimal Price, string Name,int MaxVolume);
