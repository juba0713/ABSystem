using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABSystem.Services.Dto
{
    public class ViewDto
    {
        /*
         *  This is DTO for booking
         */
        public UserBookDto UserBookDto { get; set; } = new UserBookDto();

        /*
         *  This is Dto for Showing Booking Details
         */
        public RoomDto RoomDto { get; set; } = new RoomDto();

        public IEnumerable<UserBookDto> UserBooks { get; set; } = new List<UserBookDto>();

        public List<RoomDto>? Rooms { get; set; } = new List<RoomDto>();

        public List<RoomDto>? PopularRooms {  get; set; } = new List<RoomDto> ();

        public int CountRooms {  get; set; }

        public int CountBookings {  get; set; }

        public int CountPendingBookings {  get; set; }

        public int CountAcceptedBookings {  get; set; }

        public int CountRejectedBookings {  get; set; }

        public int CountUsers {  get; set; }
    }
}
