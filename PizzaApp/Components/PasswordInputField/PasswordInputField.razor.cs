using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PizzaApp.Components
{
    public partial class PasswordInputField
    {
        [Parameter]
        public bool ShowConfirmPasswordField { get; set; }

        [Parameter] 
        public EventCallback<string> ReturnPassword { get; set; }

        MudTextField<string>? PasswordField;
        bool ShowPassword { get; set; } = true;
        string Password { get; set; } = string.Empty;        
        InputType PasswordInput { get; set; } = InputType.Password;
        string PasswordInputIcon { get; set; } = Icons.Material.Filled.VisibilityOff;

        private static string? PasswordStrength(string pw)
        {
            if (string.IsNullOrWhiteSpace(pw))
            {
                return "Password is required!";
            }
            if (pw.Length < 8)
                return "Password must be at least of length 8";
            if (!Regex.IsMatch(pw, @"[A-Z]"))
                return "Password must contain at least one capital letter";
            if (!Regex.IsMatch(pw, @"[a-z]"))
                return "Password must contain at least one lowercase letter";
            if (!Regex.IsMatch(pw, @"[0-9]"))
                return "Password must contain at least one digit";
            return null;
        }

        private string? PasswordMatch(string tmpPassword)
        {
            if (string.IsNullOrWhiteSpace(tmpPassword))
                return "Password is required!";

            if (PasswordField?.Value != tmpPassword)
                return "Passwords don't match";
            return null;
        }

        private void TogglePasswordVisibility()
        {
            ShowPassword = !ShowPassword;
            if (ShowPassword)
            {
                PasswordInput = InputType.Password;
                PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
            }
            else
            {
                PasswordInput = InputType.Text;
                PasswordInputIcon = Icons.Material.Filled.Visibility;
            }
        }
    }
}
