using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaApp.Attributes;
using Microsoft.AspNetCore.Components;
using PizzaApp.Server.DAL.Models;
using System.Text.RegularExpressions;

namespace PizzaApp.Models
{
    public class CreateUserModel
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(25, ErrorMessage = "Name length can't be more than 25.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(25, ErrorMessage = "Name length can't be more than 25.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string Email { get; set; } = string.Empty;

        private string password = string.Empty;
        [Required(ErrorMessage = "Password is required")]
        [PasswordValidation]
        public string Password
        {
            get => password;
            set
            {
                password = value;
                FillPasswordCheckList();
            }
        }

        private string confirmPassword = string.Empty;
        [Required(ErrorMessage = "Password is required")]
        [Compare(nameof(Password),ErrorMessage = "Password fields do not match")]
        public string ConfirmPassword
        {
            get => confirmPassword;
            set
            {
                confirmPassword = value;
                FillConfirmPasswordCheckList();
            }
        }

        public string ProfilePicture { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required")]
        [BirthdayValidation]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNumber { get; set; } = string.Empty;

        public bool PasswordInRange { get; set; }

        public bool PasswordLowerCase { get; set; }

        public bool PasswordUpperCase { get; set; }

        public bool PasswordSpecialChar { get; set; }

        public bool PasswordHasNumber { get; set; }

        public bool PasswordsMatch { get; set; }

        /// <summary>
        /// Validation to check off bools for visual aid on meeting requirements
        /// </summary>
        private void FillPasswordCheckList()
        {
            if (Password.Length >= 8 && Password.Length <= 30)
            {
                PasswordInRange = true;
            }
            else
            {
                PasswordInRange = false;
            }

            Regex lowerCase = new(@"[a-z]+");
            if (lowerCase.IsMatch(Password))
            {
                PasswordLowerCase = true;
            }
            else
            {
                PasswordLowerCase = false;
            }

            Regex upperCase = new(@"[A-Z]+");
            if (upperCase.IsMatch(Password))
            {
                PasswordUpperCase = true;
            }
            else
            {
                PasswordUpperCase = false;
            }

            Regex specialChar = new(@"[!@#$%^&*]");
            if (specialChar.IsMatch(Password))
            {
                PasswordSpecialChar = true;
            }
            else
            {
                PasswordSpecialChar = false;
            }

            Regex number = new(@"[0-9]+");
            if (number.IsMatch(Password))
            {
                PasswordHasNumber = true;
            }
            else
            {
                PasswordHasNumber = false;
            }

            if (Password == ConfirmPassword)
            {
                PasswordsMatch = true;
            }
            else
            {
                PasswordsMatch = false;
            }
        }

        /// <summary>
        /// Validation to check off bools for visual aid on meeting requirements
        /// </summary>
        private void FillConfirmPasswordCheckList()
        {
            if (Password == ConfirmPassword)
            {
                PasswordsMatch = true;
            }
            else
            {
                PasswordsMatch = false;
            }
        }
    }
}
