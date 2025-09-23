using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using PerformanceReport.Models;
using PerformanceReport.Services;

namespace PerformanceReplft.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TradeController : ControllerBase
    {
        private readonly TradeService _tradeService;
        public TradeController(TradeService tradeService)
        {
            _tradeService =tradeService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveTradeAsync([FromBody] TradeRequest trades)
        {
            Console.WriteLine(trades);
            var saved = await _tradeService.SaveTradeAsync(trades);
            return CreatedAtAction(nameof(GetAllTradesAsync), null, saved);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTradesAsync()
        {
            var t = await _tradeService.GetAllTrades();
            return Ok(t);
        }
    }
}

