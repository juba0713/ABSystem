using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace ABSystem.Data.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Status { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email {  get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public DateTime BookDate { get; set; }

        public TimeSpan StartTime {  get; set; }
        
        public TimeSpan EndTime { get; set; }

        public string? Request { get; set; }

        public int RoomId { get; set; }

        [ForeignKey("RoomId")]
        [JsonIgnore]
        public Room? Room { get; set; }

        public string UserId { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate {  get; set; }

        public int IsDeleted { get; set; }
    }
}
