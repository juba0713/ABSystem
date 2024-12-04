﻿using ABSystem.Resources.Constants;
using ABSystem.Services.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABSystem.Services.Dto
{
    public class UserBookDto
    {

        public int RoomId { get; set; }

        [Required(ErrorMessage = "Error FirstName")]
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "BookDate is required.")]
        [ValidateBookDate]
        public DateTime BookDate { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public string Request { get; set; } = string.Empty;

    }
}