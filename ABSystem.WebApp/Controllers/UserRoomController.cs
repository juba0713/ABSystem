using ABSystem.Resources.Constants;
using ABSystem.Services.Dto;
using ABSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ABSystem.WebApp.Controllers
{
    [Authorize(Roles = CommonConstant.User)]
    public class UserRoomController : Controller
    {

        private readonly IRoomService _roomService;
        private readonly ILogger<UserRoomController> _logger;

        public UserRoomController(ILogger<UserRoomController> logger, IRoomService roomService) 
        { 
            _roomService = roomService;
            _logger = logger;
        }

        [HttpGet]
        [Route("/rooms")]
        public IActionResult RoomsListScreen()
        {

            List<RoomDto> rooms = null;

            RoomListDto roomListDto = new RoomListDto();

            try
            {
                rooms = this._roomService.GetRoomsWithImage();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, MessageConstant.ROOM_NOT_FOUND);
            }

            roomListDto.Rooms = rooms;
            return PartialView("~/Views/User/RoomList.cshtml", roomListDto);
        }

        [HttpGet]
        [Route("/rooms/room-details")]
        public IActionResult RoomDetailsScreen([FromQuery] int roomId)
        {

            //RoomDto room = null;
            ViewDto viewDto = new ViewDto();

            try
            {
                var room = this._roomService.GetRoomByIdWithBookings(roomId);

                viewDto.RoomDto = room;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, MessageConstant.ROOM_NOT_FOUND);
            }

            return PartialView(CommonConstant.U_ROOMS_DETAILS_HTML, viewDto);
        }
    }
}
