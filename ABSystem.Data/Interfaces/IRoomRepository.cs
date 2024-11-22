using ABSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/**
 * @Author Ramz T.
 * @Added 22/11/2024
 */
namespace ABSystem.Data.Interfaces
{
    public interface IRoomRepository
    {
        void AddRoom(Room room);

        void UpdateRoom(Room room);

        IEnumerable<Room> GetRooms();
        Room? GetUser(int roomId);

        void DeleteRoom(int roomId);
    }
}
