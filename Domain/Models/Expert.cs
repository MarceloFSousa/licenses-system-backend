namespace Domain.Models
{
    public class Expert
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime InitDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
        //public ICollection<Negocio> Negocios { get; set; } = new List<Negocio>();
    }
}
