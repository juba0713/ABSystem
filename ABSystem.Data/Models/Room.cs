using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABSystem.Data.Models
{
    public class Room
    {
        public string? Id { get; set; }

        public string? Name { get; set; }

        public string? Facility { get; set; }

        public int Capacity { get; set; }

        public string? Address { get; set; }

        public string? Description { get; set; }

        public string[]? images { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public int IsDeleted { get; set; }
    }
}

