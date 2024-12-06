using ABSystem.Resources.Constants;
using ABSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ABSystem.WebApp.Controllers
{
    public class NotificationController : Controller
    {

        private readonly INotificationService _notificationService;
        private readonly ILogger<NotificationController> _logger;

        public NotificationController(INotificationService notificationService, ILogger<NotificationController> logger)
        {
            _notificationService = notificationService;
            _logger = logger;
        }

        [HttpGet]
        [Route("/admin/notification/retrieve")]
        [Authorize(Roles = CommonConstant.Admin + "," + CommonConstant.Super)]
        public IActionResult AdminNotification()
        {
            var notifications = this._notificationService.GetNotifications();
            Console.WriteLine("Hi");
            return Json(notifications);
        }

        [HttpGet]
        [Route("/notification/retrieve")]
        [Authorize(Roles = CommonConstant.User)]
        public IActionResult UserNotification()
        {
            var notifications = this._notificationService.GetNotificationsByUserId();
            Console.WriteLine("Hi");
            return Json(notifications);
        }
    }
}
