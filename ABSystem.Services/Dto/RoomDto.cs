using ABSystem.Data.Models;
using ABSystem.Resources.Constants;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABSystem.Services.Dto
{
    public class RoomDto
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "*" + MessageConstant.ROOM_NAME_REQUIRED)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "*" + MessageConstant.ROOM_FACILITY_REQUIRED)]
        public string Facility {  get; set; } = string.Empty;

        [Required(ErrorMessage = "*" + MessageConstant.ROOM_CAPACITY_REQUIRED)]
        public string Capacity {  get; set; } = string.Empty;

        [Required(ErrorMessage = "*" + MessageConstant.ROOM_ADDRESS_REQUIRED)]
        public string Address {  get; set; } = string.Empty;

        [Required(ErrorMessage = "*" + MessageConstant.ROOM_DESCRIPTION_REQUIRED)]
        public string Description {  get; set; } = string.Empty;

        public List<IFormFile>? Images { get; set; }

        public List<string>? ImagesPath { get; set; }

        /*
         * First Image is the Image that will be displayed in the Room List of the User
         */
        public string ImagePath { get; set; } = string.Empty;

        public ICollection<Book> Bookings { get; set; } = new List<Book>();


        /*
         *  This is DTO for booking
         */
        public UserBookDto UserBookDto { get; set; } = new UserBookDto();

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
