using ABSystem.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABSystem.Services.Annotations
{
    public class PasswordRequiredOnCreateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            // Access the object instance being validated
            var instance = validationContext.ObjectInstance as UserDto;

            // Assume that Id == 0 means it's a new record (create operation)
            if (instance != null && instance.Id == 0)
            {
                Console.WriteLine("AWAW");
                // Check if the password is null or empty
                if (string.IsNullOrEmpty(value as string))
                {
                    return new ValidationResult(ErrorMessage ?? "Password is required for new users.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
