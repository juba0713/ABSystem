using ABSystem.Resources.Constants;
using ABSystem.Services.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

/**
 * @Author Julius.B
 * @Added 09/11/2024
 */
namespace ABSystem.Services.Dto
{
    public class UserDto
    {

        public String? Id { get; set; }

        [Required(ErrorMessage = "*" + MessageConstant.EMAIL_REQUIRED)]
        [EmailAddress(ErrorMessage = "*" + MessageConstant.EMAIL_FORMAT)]
        public string? Email { get; set; }

        [PasswordRequiredOnCreate(ErrorMessage = "*" + MessageConstant.PASSWORD_REQUIRED)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "*" + MessageConstant.PASSWORD_LENGTH)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "*" + MessageConstant.FIRST_NAME_REQUIRED)]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "*" + MessageConstant.LAST_NAME_REQUIREED)]
        public string? LastName { get; set; }


        public string? PhoneNumber {  get; set; }

        public string? Address { get; set; }

        public string? ConfirmPassword {  get; set; }

        public string? Gender {  get; set; }

        [Required(ErrorMessage = "*" + MessageConstant.ROLE_REQUIRED)]
        [RequiredDropdown("*" + MessageConstant.ROLE_REQUIRED)]
        public string? Role { get; set; }
    }
}
