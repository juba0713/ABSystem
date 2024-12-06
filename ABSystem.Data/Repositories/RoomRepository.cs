using ABSystem.Data.Interfaces;
using ABSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABSystem.Data.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ABSystemDbContext _context;

        public RoomRepository(ABSystemDbContext context)
        {
            _context = context;
        }

        public int AddRoom(Room room)
        {
            this._context.Rooms.Add(room);

            this._context.SaveChanges();

            return room.Id;
        }

        public void DeleteRoom(Room room)
        {
            this._context.Rooms.Remove(room);

            this._context.SaveChanges();
        }

        public void EditRoom(Room room)
        {
            this._context.Rooms.Update(room);

            this._context.SaveChanges();
        }

        public Room GetRoomById(int roomId)
        {
            return this._context.Rooms.Find(roomId)!;
        }

        public IEnumerable<Room> GetRooms()
        {
            return this._context.Rooms.ToList();
        }

        public Room GetRoomByIdWithBookings(int roomId)
        {
            return this._context.Rooms
                .Include(r => r.Bookings) 
                .Where(r => r.Id == roomId && r.IsDeleted == 0) 
                .FirstOrDefault()!;
        }

        public IEnumerable<Room> GetPopularRooms()
        {
            return this._context.Rooms
                .Include(r => r.Bookings) // Include related bookings
                .Where(r => r.IsDeleted == 0) // Exclude deleted rooms
                .OrderByDescending(r => r.Bookings.Count) // Order by the number of bookings
                .Take(4) // Limit to top 4 rooms
                .ToList(); // Execute the query and return as a list
        }
    }
}
