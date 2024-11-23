using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ABSystem.WebApp.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        [HttpGet]
        [Route("/book")]
        public IActionResult BookingListScreen()
        {
            return PartialView("~/Views/User/RoomList.cshtml");
        }
    }
}
