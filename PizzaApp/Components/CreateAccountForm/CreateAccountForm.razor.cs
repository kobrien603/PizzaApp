using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using PizzaApp.Server.DAL;
using PizzaApp.Server.DAL.Models;
using PizzaApp.Server.Helpers;
using PizzaApp.Server.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static MudBlazor.Colors;

namespace PizzaApp.Components
{
    public partial class CreateAccountForm
    {
        CreateUserModel NewUser { get; set; } = new();
        EditForm? MudForm { get; set; }
        bool BtnCreateAccount { get; set; }
        bool IsLoading { get; set; } = true;
        bool ShowPassword { get; set; } = true;
        InputType PasswordInput { get; set; } = InputType.Password;
        string PasswordInputIcon { get; set; } = Icons.Material.Filled.VisibilityOff;

        [Inject] IDbContextFactory<PizzaContext> PizzaContext { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                IsLoading = false;
                StateHasChanged();
            }

            base.OnAfterRender(firstRender);
        }

        private void SetProfilePicture(string profilePic)
        {
            NewUser.ProfilePicture = profilePic;
            StateHasChanged();
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
            StateHasChanged();
        }

        private async Task CreateAccount()
        {
            BtnCreateAccount = true;
            string hashPassword = PasswordHelper.CreateHashPassword(NewUser.Password);

            User dbUser = new()
            {
                ID = 0, // always 0 to generate new id in db
                Password = PasswordHelper.CreateHashPassword(NewUser.Password),
                CreatedDate = DateTime.Now,
                DateOfBirth = NewUser.DateOfBirth,
                Email = NewUser.Email,
                FirstName = NewUser.FirstName,
                LastName = NewUser.LastName,
                ModifiedDate = DateTime.Now,
                PhoneNumber = NewUser.PhoneNumber,
                ProfilePicture = NewUser.ProfilePicture,
            };

            using var context = PizzaContext.CreateDbContext();
            using var repository = new PizzaRepository(context);

            var response = repository.Users.InsertOrUpdate(dbUser);

            if (response)
            {
                // navigate
            }
            else
            {
                // error
            }

            await Task.Delay(1000);

            BtnCreateAccount = false;
        }
    }
}
