using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABSystem.Services.Annotations
{
    public class ValidateBookDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime date && date == default(DateTime)) // default(DateTime) = 01/01/0001
            {
                return new ValidationResult("Please select a valid booking date.");
            }

            return ValidationResult.Success!;
        }
    }
}
