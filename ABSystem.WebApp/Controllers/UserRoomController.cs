﻿using ABSystem.Resources.Constants;
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
        [Route("/rooms-list")]
        public IActionResult RoomsListScreen()
        {

            ViewDto viewDto = new ViewDto();

            RoomListDto roomListDto = new RoomListDto();

            try
            {
                var rooms = this._roomService.GetRoomsWithImage();
                var popularRooms = this._roomService.GetPopularRoomsWithImage();

                viewDto.Rooms = rooms;
                viewDto.PopularRooms = popularRooms;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, MessageConstant.ROOM_NOT_FOUND);
            }

            //roomListDto.Rooms = rooms;
            return PartialView("~/Views/User/RoomList.cshtml", viewDto);
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
