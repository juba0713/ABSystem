﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * @Author Julius.B
 * @Added 09/11/2024
 */
namespace ABSystem.Data.Models
{
    public class User : IdentityUser
    {
        //public int Id { get; set; }

        //public string? EmailAddress {  get; set; }

        public string Password { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Gender {  get; set; } = string.Empty;

        public string Address {  get; set; } = string.Empty;

        //public string? Role { get; set; }

        public DateTime CreatedDate {  get; set; }

        public DateTime UpdatedDate { get; set; }

        public int IsDeleted { get; set; }
    }
}
