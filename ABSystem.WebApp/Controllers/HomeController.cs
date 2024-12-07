using ABSystem.Data.Models;
using ABSystem.Resources.Constants;
using ABSystem.Services.Dto;
using ABSystem.Services.Interfaces;
using ABSystem.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IBookService _bookService;
        private readonly IRoomService _roomService;
        private readonly UserManager<User> _userManager;
        private readonly INotificationService _notificationService;

        public HomeController(ILogger<HomeController> logger, 
            IRoomService roomService, 
            IBookService bookService,
            UserManager<User> userManager,
            INotificationService notificationService)
        {
            _logger = logger;
            _roomService = roomService;
            _bookService = bookService; 
            _userManager = userManager;
            _notificationService = notificationService;
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
        public async Task<IActionResult> AdminDashboard()
        {
            ViewDto viewDto = new ViewDto();

            viewDto.CountRooms = this._roomService.CountRooms();
            viewDto.CountBookings = this._bookService.CountBooking();
            viewDto.CountPendingBookings = this._bookService.CountPendingBooking();
            viewDto.CountAcceptedBookings = this._bookService.CountAcceptedBooking();
            viewDto.CountRejectedBookings = this._bookService.CountRejectedBooking();
            viewDto.CountUsers = await this._userManager.Users.CountAsync();
            viewDto.MonthlyCountBookings = this._bookService.MonthlyCountBooking();



            return PartialView("~/Views/Admin/Dashboard.cshtml", viewDto);
        }

        [Authorize(Roles = CommonConstant.Super)]
        [HttpGet]
        [Route("/super/dashboard")]
        public async Task<IActionResult> SuperDashboard()
        {

            ViewDto viewDto = new ViewDto();

            viewDto.CountRooms = this._roomService.CountRooms();
            viewDto.CountBookings = this._bookService.CountBooking();
            viewDto.CountPendingBookings = this._bookService.CountPendingBooking();
            viewDto.CountAcceptedBookings = this._bookService.CountAcceptedBooking();
            viewDto.CountRejectedBookings = this._bookService.CountRejectedBooking();
            viewDto.CountUsers = await this._userManager.Users.CountAsync();
            viewDto.Notifications = this._notificationService.GetAllNotifications();
            viewDto.UserNotifications = this._notificationService.GetRecentlyUserNotifications();
            viewDto.MonthlyCountBookings = this._bookService.MonthlyCountBooking();
            return PartialView("~/Views/Super/Dashboard.cshtml", viewDto);
        }

        [Authorize(Roles = CommonConstant.User)]
        [HttpGet]
        [Route("/dashboard")]
        public IActionResult Dashboard()
        {
            ViewDto viewDto = new ViewDto();
            try
            {

                viewDto.FiveRecentlyPendingBooking = this._bookService.GetFiveRecentlyPendingBooking();
                viewDto.FiveRecentlyAcceptedBooking = this._bookService.GetFiveRecentlyAcceptedBooking();
                viewDto.FiveUpComingBooking = this._bookService.GetFiveUpComingBooking();


                return PartialView("~/Views/User/Dashboard.cshtml", viewDto);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, MessageConstant.ERROR);
                return PartialView("~/Views/User/Dashboard.cshtml", viewDto);
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
