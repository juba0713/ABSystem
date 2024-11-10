using ABSystem.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

/**
 * @Author Julius.B
 * @Added 09/11/2024
 */
namespace ABSystem.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        [Route("/admin/dashboard")]
        public IActionResult AdminDashboard()
        {
            return PartialView("~/Views/Admin/Dashboard.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
