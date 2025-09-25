
namespace PerformanceReport.Models
{
    public class Trade
    {
        public Guid Id { get; set; }
        public int Ticket { get; set; }
        public string Symbol { get; set; }
        public int Time { get; set; }
        public decimal Volume { get; set; }
        public decimal Price { get; set; }
        public decimal Profit { get; set; }
        public int Type { get; set; }
        public long Magic { get; set; }
    }
}
