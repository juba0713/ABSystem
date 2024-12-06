using ABSystem.Data.Models;
using ABSystem.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABSystem.Services.Interfaces
{
    public interface IRoomService
    {
        /*
         * This method is for adding a room to the database
         */
        public void AddRoom(RoomDto dto);

        /*
         * This method is for getting all the rooms from the database
         */
        public IEnumerable<Room> GetRooms();

        /*
         * This method is for getting a room by its id from the database
         */
        public void EditRoom(RoomDto dto);

        /*
         * This method is for saving the edited room to the database
         */
        public RoomDto GetRoomById(int roomId);

        /*
        * This method is for deleting a room from the database
        */
        public void DeleteRoom(int roomId);

        /*
         * This method is for getting a room by its id from the database with bookings
         */
        public List<RoomDto> GetRoomsWithImage();

        /*
         * This method is for getting a room by its id from the database with bookings
         */
        public RoomDto GetRoomByIdWithBookings(int roomId);

        /*
         * This method is for getting a popular room
         */
        public List<RoomDto> GetPopularRoomsWithImage();
    }
}
