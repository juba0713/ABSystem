using ABSystem.Resources.Constants;
using ABSystem.Services.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABSystem.Services.Dto
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "*" + MessageConstant.EMAIL_REQUIRED)]
        [EmailAddress(ErrorMessage = "*" + MessageConstant.EMAIL_FORMAT)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "*" + MessageConstant.PASSWORD_REQUIRED)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "*" + MessageConstant.PASSWORD_LENGTH)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "*" + MessageConstant.CONFIRM_PASSWORD_REQUIRED)]
        [Compare("Password", ErrorMessage = "*" + MessageConstant.PASSWORD_MISMATCH)]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "*" + MessageConstant.FIRST_NAME_REQUIRED)]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "*" + MessageConstant.LAST_NAME_REQUIREED)]
        public string? LastName { get; set; }


    }
}
