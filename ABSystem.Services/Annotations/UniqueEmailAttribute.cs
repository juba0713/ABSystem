using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABSystem.Services.Annotations
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string email)
            {
                // Access UserManager via DI
                var userManager = (UserManager<IdentityUser>)validationContext.GetService(typeof(UserManager<IdentityUser>))!;
                if (userManager == null)
                {
                    throw new InvalidOperationException("UserManager service not available.");
                }

                // Check for duplicate emails
                var user = userManager.FindByEmailAsync(email).Result;
                if (user != null)
                {
                    return new ValidationResult(ErrorMessage ?? $"{email} is already in use.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
