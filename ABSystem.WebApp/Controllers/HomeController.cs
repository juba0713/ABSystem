using ABSystem.Resources.Constants;
using ABSystem.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

/**
 * @Author Julius.B
 * @Added 09/11/2024
 */
namespace ABSystem.WebApp.Controllers
{
    [Authorize]
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

        [Authorize(Roles = CommonConstant.Admin)]
        [HttpGet]
        [Route("/admin/dashboard")]
        public IActionResult AdminDashboard()
        {
            // Get user details from session
            var userId = HttpContext.Session.GetString("UserId");
            var userEmail = HttpContext.Session.GetString("UserEmail");
            var userRoles = HttpContext.Session.GetString("UserRoles");
            Console.WriteLine("User ID: " + userId);
            Console.WriteLine("User Email: " + userEmail);
            Console.WriteLine("User Roles: " + userRoles);
            return PartialView("~/Views/Admin/Dashboard.cshtml");
        }

        [Authorize(Roles = CommonConstant.Super)]
        [HttpGet]
        [Route("/super/dashboard")]
        public IActionResult SuperDashboard()
        {
            return PartialView("~/Views/Super/Dashboard.cshtml");
        }

        [Authorize(Roles = CommonConstant.User)]
        [HttpGet]
        [Route("/dashboard")]
        public IActionResult Dashboard()
        {
            return PartialView("~/Views/User/Dashboard.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
