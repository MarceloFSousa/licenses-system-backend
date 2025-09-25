using PerformanceReport.Models;
using PerformanceReport.Services;

namespace PerformanceReport.Utils
{
    public static class PerformanceUtil
    {
        public static decimal GetProfitFactor(List<Trade> trades)
        {
            decimal grossProfit = trades.Where(t => t.Profit > 0).Sum(t => t.Profit);
            decimal grossLoss = trades.Where(t => t.Profit < 0).Sum(t => t.Profit);

            if (grossLoss == 0 || grossProfit == 0)
                return 0;

            return grossProfit / Math.Abs(grossLoss);
        }
        public static int GetNumOfTrades(List<Trade> trades)
        {
            return trades.Count;
        }
        public static int GetWinningTrades(List<Trade> trades)
        {
            return trades.Count(t => t.Profit > 0);
        }
        public static int GetLosingTrades(List<Trade> trades)
        {
            return trades.Count(t => t.Profit < 0);
        }
        public static decimal GetWorstTrade(List<Trade> trades)
        { 
            return trades.Min(t => t.Profit);
        }
        public static decimal GetBestTrade(List<Trade> trades)
        {
            return trades.Max(t => t.Profit);
        }
        public static decimal GetAverageProfit(List<Trade> trades)
        {
            return trades.Average(t => t.Profit);
        }
        public static decimal GetDrawdown(List<Trade> trades)
        {
            if (trades == null || trades.Count == 0)
                return 0;

            trades = trades.OrderBy(t => t.Time).ToList();

            decimal balance = 0;
            decimal peak = 0;
            decimal maxDrawdown = 0;

            foreach (var trade in trades)
            {
                balance += trade.Profit;

                if (balance > peak)
                    peak = balance;

                var drawdown = peak - balance;

                if (drawdown > maxDrawdown)
                    maxDrawdown = drawdown;
            }

            return maxDrawdown;
        }

    }
}
