using Microsoft.AspNetCore.Components;
using MudBlazor;
using PizzaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaApp.Pages.Account
{
    public partial class Create
    {
        [CascadingParameter] ISnackbar Snackbar { get; set; }
        //[Inject] IDbContextFactory<PizzaContext> PizzaContext { get; set; }

        CreateUserModel NewUser { get; set; } = new();
        bool BtnCreateAccount { get; set; }
        bool IsLoading { get; set; } = true;
        bool ShowPassword { get; set; } = true;
        InputType PasswordInput { get; set; } = InputType.Password;
        string PasswordInputIcon { get; set; } = Icons.Material.Filled.VisibilityOff;

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

            //using var context = PizzaContext.CreateDbContext();
            //using var repository = new PizzaRepository(context);

            //// check if email is already in use
            //if (repository.Users.EmailAlreadyRegistered(NewUser.Email))
            //{
            //    Snackbar.Add("Email has already been registered. Please try again", Severity.Error);
            //}
            //else
            //{

            //    User dbUser = new()
            //    {
            //        ID = 0, // always 0 to generate new id in db
            //        Password = PasswordHelper.CreateHashPassword(NewUser.Password),
            //        CreatedDate = DateTime.Now,
            //        DateOfBirth = NewUser.DateOfBirth,
            //        Email = NewUser.Email,
            //        FirstName = NewUser.FirstName,
            //        LastName = NewUser.LastName,
            //        ModifiedDate = DateTime.Now,
            //        PhoneNumber = NewUser.PhoneNumber,
            //        ProfilePicture = NewUser.ProfilePicture,
            //    };

            //    var response = repository.Users.InsertOrUpdate(dbUser);

            //    if (response)
            //    {
            //        // navigate
            //    }
            //    else
            //    {
            //        // error
            //    }
            //}

            await Task.Delay(1000);

            BtnCreateAccount = false;
            StateHasChanged();
        }
    }
}
