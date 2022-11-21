using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using PizzaApp.Server.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PizzaApp.Components
{
    public partial class CreateAccountForm
    {
        User User { get; set; } = new();
        EditForm? MudForm { get; set; }
        bool BtnCreateAccount { get; set; }
        MudTextField<string>? Password { get; set; }
        bool IsLoading { get; set; } = true;

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                IsLoading = false;
                StateHasChanged();
            }

            base.OnAfterRender(firstRender);
        }

        private IEnumerable<string> PasswordStrength(string pw)
        {
            if (string.IsNullOrWhiteSpace(pw))
            {
                yield return "Password is required!";
                yield break;
            }
            if (pw.Length < 8)
                yield return "Password must be at least of length 8";
            if (!Regex.IsMatch(pw, @"[A-Z]"))
                yield return "Password must contain at least one capital letter";
            if (!Regex.IsMatch(pw, @"[a-z]"))
                yield return "Password must contain at least one lowercase letter";
            if (!Regex.IsMatch(pw, @"[0-9]"))
                yield return "Password must contain at least one digit";
        }

        private string? PasswordMatch(string tmpPassword)
        {
            if (string.IsNullOrWhiteSpace(tmpPassword))
                return "Password is required!";

            if (Password?.Value != tmpPassword)
                return "Passwords don't match";
            return null;
        }

        private async Task CreateAccount()
        {
            BtnCreateAccount = true;

            await Task.Delay(1000);

            BtnCreateAccount = false;
        }

        private void SetProfilePicture(string profilePic)
        {
            User.ProfilePicture = profilePic;
            StateHasChanged();
        }
    }
}
