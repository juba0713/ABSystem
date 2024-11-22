using ABSystem.Data.Interfaces;
using ABSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * @Author Ramz.T
 * @Added 22/11/2024
 */
namespace ABSystem.Data.Repositories
{

    public class RoomRepository : IRoomRepository
    {
        private readonly ABSystemDbContext _context;

        public RoomRepository(ABSystemDbContext context)
        {
            _context = context;
        }

        public void AddRoom(Room room)
        {
            _context.Rooms.Add(room);
            _context.SaveChanges();
        }

        public void DeleteRoom(int roomId)
        {
            var room = _context.Rooms.Find(roomId);
            if (room != null)
            {
                _context.Rooms.Remove(room);
                _context.SaveChanges();
            }
        }
        public void UpdateRoom(Room room)
        {
            var existingRoom = _context.Rooms.FirstOrDefault(u => u.Id == room.Id);
            if (existingRoom != null)
            {
                existingRoom.Name = room.Name;
                existingRoom.Address = room.Address;
                existingRoom.Capacity = room.Capacity;
                existingRoom.Facility = room.Facility;

                _context.SaveChanges();
            }
        }

        public IEnumerable<Room> GetRooms()
        {
            return _context.Rooms.ToList();
        }

        public Room? GetUser(int roomId)
        {
            throw new NotImplementedException();
        }
    }
}
