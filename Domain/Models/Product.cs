namespace Domain.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int MaxVolume { get; set; }
        public int ExpertId { get; set; }
        public DateTime CreatedAt { get; set; }

        // Relações
        public Expert Expert { get; set; } = null!;
        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
        public ICollection<License> Licenses { get; set; } = new List<License>();
    }
}
