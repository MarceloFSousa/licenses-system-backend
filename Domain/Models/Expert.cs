using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Expert
    {
        public Guid  Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImgUrl { get; set; } = null!;
        public string FileContentUrl { get; set; } = null!;
        public DateTime InitDate { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
        //public ICollection<Negocio> Negocios { get; set; } = new List<Negocio>();
    }
}
public record ExpertPutRequest(string? Name, string? Description, DateTime? InitDate);
