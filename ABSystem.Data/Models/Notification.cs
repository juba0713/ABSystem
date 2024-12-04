using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABSystem.Data.Models
{
    public class Notification
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public int RoomId { get; set; }

        public string Message { get; set; } = string.Empty;

        public int IsRead { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public int IsDeleted { get; set; }
    }
}
