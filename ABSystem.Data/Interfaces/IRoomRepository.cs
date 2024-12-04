using ABSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABSystem.Data.Interfaces
{
    public interface IRoomRepository
    {
        /*
         * This method is for adding a room to the database
         */
        public int AddRoom(Room room);

        /*
         * This method is for getting all the rooms from the database
         */
        public IEnumerable<Room> GetRooms();

        /*
         * This method is for getting a room by its id from the database
         */
        public Room GetRoomById(int roomId);

        /*
         * This method is for saving the edited room to the database
         */
        public void EditRoom(Room room);

        /*
         * This method is for deleting a room from the database
         */
        public void DeleteRoom(Room room);

        /*
         * This method is for getting a room by its id from the database with bookings
         */
        public Room GetRoomByIdWithBookings(int roomId);

    }
}
