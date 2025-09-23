using Microsoft.AspNetCore.Mvc;

namespace PerformanceReport.Controllers
{
    public class PerformanceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
