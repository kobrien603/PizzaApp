using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PizzaApp.Server.Attributes
{
    /// <summary>
    /// Validate the user has entered a valid birthday
    /// </summary>
    public class BirthdayValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null && DateTime.TryParse(value.ToString(),out DateTime date))
            {
                DateTime today = DateTime.Today;
                
                int age = today.Year - date.Year;
                if (date > today.AddYears(-age))
                    age--;

                if (age < 18)
                {
                    return new ValidationResult("You must be 18 years or older to create an account.");
                }
                else
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult("Date is not valid");
        }
    }
}
