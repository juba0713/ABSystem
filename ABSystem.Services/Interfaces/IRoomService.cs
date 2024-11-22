using ABSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABSystem.Services.Interfaces
{
    public interface IRoomService
    {
        void AddRoom(Room room);

        void UpdateRoom(Room room);

        IEnumerable<Room> GetRooms();
        Room? GetUser(int roomId);

        void DeleteRoom(int roomId);
    }
}
