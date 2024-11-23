using ABSystem.Resources.Constants;
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

        [Authorize(Roles = CommonConstant.User)]
        [Route("/room")]
        public IActionResult RoomListScreen()
        {

            return PartialView("~/Views/User/RoomList.cshtml");
        }
    }
}
