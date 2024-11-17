using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ABSystem.WebApp.Controllers
{
    [Authorize]
    public class RoomController : Controller
    {
        [HttpGet]
        public IActionResult RoomsListScreen()
        {
            return PartialView("~/Views/Admin/RoomsList.cshtml");
        }

        [HttpGet]
        public IActionResult AddRoomScreen()
        {
            return PartialView("~/Views/Admin/AddRoom.cshtml");
        }
    }
}
