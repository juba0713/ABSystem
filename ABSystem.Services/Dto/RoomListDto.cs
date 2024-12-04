using ABSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABSystem.Services.Dto
{
    public class RoomListDto
    {
        public IEnumerable<RoomDto>? Rooms {  get; set; }

        public IEnumerable<RoomDto>? PopularRooms { get; set; }
    }
}
