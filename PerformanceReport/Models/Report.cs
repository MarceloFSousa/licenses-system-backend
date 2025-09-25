namespace PerformanceReport.Models
{
    public class Report
    {
        public int Trades { get; set; }
        public int WinningTrades { get; set; }
        public int LosingTrades { get; set; }
        public decimal BestTrade { get; set; }
        public decimal ProfitFactor { get; set; }
        public decimal Drawdown { get; set; }
        public decimal AverageProfit { get; set; }
        public decimal WorstTrade { get; set; }
    }
}