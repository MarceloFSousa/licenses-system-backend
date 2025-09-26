using Microsoft.AspNetCore.Mvc;
using PerformanceReport.Models;
using PerformanceReport.Repositories;

namespace PerformanceReport.Controllers
{
    public class PerformanceController : Controller
    {
        private readonly PerformanceService _service; 
        public PerformanceController(PerformanceService service) { _service = service; }
        [HttpGet("{id:int}")]
        public ActionResult<Report> GetReportFromExpert(int id)
        {
            return Ok(_service.GetPerformanceReportFromExpert(id);
        }
    }
}
