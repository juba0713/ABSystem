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
        public int AddRoom(Room room);

        public IEnumerable<Room> GetRooms();

        public Room GetRoomById(int roomId);

        public void EditRoom(Room room);

        public void DeleteRoom(Room room);

    }
}
