using ABSystem.Resources.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ABSystem.WebApp.Controllers
{
    [Authorize(Roles = CommonConstant.User)]
    public class UserRoomController : Controller
    {     
        [Route("/room")]
        public IActionResult RoomListScreen()
        {

            return PartialView("~/Views/User/RoomList.cshtml");
        }
    }
}
