using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABSystem.Data.Objects
{
    public class BookObj
    {
        public int Id { get; set; }

        public string? Status { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public DateTime BookDate { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public string? Request { get; set; }

        public int IsDeleted { get; set; }

        public string? RoomName { get; set; }

        public int JanBookingCount { get; set; }
        public int FebBookingCount { get; set; }
        public int MarBookingCount { get; set; }
        public int AprBookingCount { get; set; }
        public int MayBookingCount { get; set; }
        public int JunBookingCount { get; set; }
        public int JulBookingCount { get; set; }
        public int AugBookingCount { get; set; }
        public int SepBookingCount { get; set; }
        public int OctBookingCount { get; set; }
        public int NovBookingCount { get; set; }
        public int DecBookingCount { get; set; }
    }
}
