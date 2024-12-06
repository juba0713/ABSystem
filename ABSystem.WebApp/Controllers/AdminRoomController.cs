using ABSystem.Data.Models;
using ABSystem.Resources.Constants;
using ABSystem.Services.Dto;
using ABSystem.Services.Interfaces;
using ABSystem.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ABSystem.WebApp.Controllers
{
    [Authorize(Roles = CommonConstant.Admin + "," + CommonConstant.Super)]
    public class AdminRoomController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly ILogger<AdminRoomController> _logger;

        public AdminRoomController(ILogger<AdminRoomController> logger, IRoomService roomService)
        {
            _logger = logger;
            _roomService = roomService;
        }

        [HttpGet]
        [Route("/admin/rooms-list")]
        public IActionResult RoomsListScreen()
        {

            var rooms = this._roomService.GetRooms();

            return PartialView(CommonConstant.A_ROOMS_LIST_HTML, rooms);
        }

        [HttpGet]
        [Route("/admin/rooms-list/add-room")]
        public IActionResult AddRoomScreen()
        {
            return PartialView(CommonConstant.A_ROOMS_ADD_HTML);
        }

        [HttpPost]
        public IActionResult AddRoom(RoomDto dto)
        {
            try
            {
                this._roomService.AddRoom(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, MessageConstant.ROOM_ADDED_ERROR);
            }
            TempData["SuccessMessage"] = MessageConstant.ADDED_ROOM;
            return RedirectToAction("RoomsListScreen");
        }

        [HttpGet]
        [Route("/admin/rooms-list/edit-room")]
        public IActionResult EditRoomScreen([FromQuery] int roomId)
        {
            RoomDto roomDto = null!;

            try
            {
                roomDto = this._roomService.GetRoomById(roomId); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, MessageConstant.ROOM_NOT_FOUND);
            }

            if (roomDto == null)
            {
                TempData["ErrorMessage"] = MessageConstant.ROOM_NOT_FOUND;
                return RedirectToAction("RoomsListScreen");  
            }

            return PartialView(CommonConstant.A_ROOMS_EDIT_HTML, roomDto);
        }

        [HttpPost]
        [Route("/admin/rooms-list/edit-room")]
        public IActionResult EditRoom(RoomDto dto)
        {
            try
            {
                this._roomService.EditRoom(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, MessageConstant.ROOM_EDITED_ERROR);
            }
            TempData["SuccessMessage"] = MessageConstant.EDITED_ROOM;
            return RedirectToAction("RoomsListScreen");
        }

        [HttpPost]
        [Route("/admin/rooms-list/delete-room")]
        public IActionResult DeleteRoom(int roomId)
        {
            try
            {
                this._roomService.DeleteRoom(roomId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, MessageConstant.ROOM_DELETED_ERROR);
            }
           
            TempData["SuccessMessage"] = MessageConstant.DELETED_ROOM;

            return RedirectToAction("RoomsListScreen");
        }
    }
}
