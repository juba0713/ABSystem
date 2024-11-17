

using ABSystem.Resources.Constants;
using ABSystem.Services.Annotations;
using System.ComponentModel.DataAnnotations;

namespace ABSystem.Services.Dto
{
    public class LoginDto
    {
        [Required(ErrorMessage = "*" + MessageConstant.EMAIL_REQUIRED)]
        [EmailAddress(ErrorMessage = "*" + MessageConstant.EMAIL_FORMAT)]
        public string? Email { get; set; }

        [PasswordRequiredOnCreate(ErrorMessage = "*" + MessageConstant.PASSWORD_REQUIRED)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "*" + MessageConstant.PASSWORD_LENGTH)]
        public string? Password { get; set; }
    }
}
