using Microsoft.AspNetCore.Mvc;
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
    /// attribute created to validate minimum password requirements before being able to submit the form
    /// </summary>
    public class PasswordValidation : ValidationAttribute
    {
        /// <summary>
        /// check password value passes all checks listed below
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            /*var password = (string)validationContext.ObjectInstance;*/
            string? password = value?.ToString();
            if (string.IsNullOrEmpty(password))
            {
                return new ValidationResult("Password is required");
            }
            else if (password.Length < 8 || password.Length > 30)
            {
                return new ValidationResult("Password must be between 8 and 30 charecters longs");
            }
            else
            {
                Regex lowerCase = new(@"[a-z]+");
                if (!lowerCase.IsMatch(password))
                {
                    return new ValidationResult("At least one lower case letter is required");
                }

                Regex upperCase = new(@"[A-Z]+");
                if (!upperCase.IsMatch(password))
                {
                    return new ValidationResult("At least one upper case letter is required");
                }

                Regex specialChar = new(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");
                if (!specialChar.IsMatch(password))
                {
                    return new ValidationResult("At least one special character is required");
                }

                Regex number = new(@"[0-9]+");
                if (!number.IsMatch(password))
                {
                    return new ValidationResult("At least one number is required");
                }
            }

            return ValidationResult.Success;
        }
    }
}
