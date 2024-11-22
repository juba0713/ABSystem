using ABSystem.Resources.Constants;
using ABSystem.Services.Dto;
using ABSystem.Services.Interfaces;
using ABSystem.Services.Objects;
using ABSystem.Data.Interfaces;
using ABSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABSystem.Services.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _repository;
        public RoomService(IRoomRepository repository)
        {
            _repository = repository;
        }
        public void AddRoom(Room room)
        {
            _repository.AddRoom(room);
        }
        public void DeleteRoom(int roomId)
        {
            _repository.DeleteRoom(roomId);
        }
        public IEnumerable<Room> GetRooms()
        {
            return _repository.GetRooms().ToList();
        }
        public Room? GetRoom(int roomId)
        {
            return _repository.GetUser(roomId);
        }
        public void UpdateRoom(Room room)
        {
            _repository.UpdateRoom(room);
        }

        public Room? GetUser(int roomId)
        {
            throw new NotImplementedException();
        }
    }
}
