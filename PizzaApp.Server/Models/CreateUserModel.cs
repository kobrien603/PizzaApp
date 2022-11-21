using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaApp.Server.Attributes;

namespace PizzaApp.Server.Models
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

        /*[Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Password must be greater than 8 characters. <br> Must contain one uppercase letter. Must one lowercase letter. Must contain one digit. Must contain one special character.")]*/
        [PasswordValidationAttribute]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [Compare(nameof(Password),ErrorMessage = "Password fields do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public string ProfilePicture { get; set; } = string.Empty;

        public DateTime? DateOfBirth { get; set; }

        public string PhoneNumber { get; set; } = string.Empty;
    }
}
