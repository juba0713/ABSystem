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
        public void AddRoom(RoomDto dto);

        public IEnumerable<Room> GetRooms(); 

        public void EditRoom(RoomDto dto);

        public RoomDto GetRoomById(int roomId);

        public void DeleteRoom(int roomId);
    }
}
