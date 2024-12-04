using ABSystem.Resources.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ABSystem.WebApp.Controllers
{
    [Authorize(Roles = CommonConstant.Admin + "," + CommonConstant.Super)]
    public class AdminBookController : Controller
    {
        [HttpGet]
        [Route("/admin/book")]
        public IActionResult BookingListScreen()
        {
            return PartialView("~/Views/User/RoomList.cshtml");
        }
    }
}
