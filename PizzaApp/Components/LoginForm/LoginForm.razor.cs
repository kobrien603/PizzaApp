using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using MudBlazor;
using PizzaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PizzaApp.Components
{
    public partial class LoginForm
    {
        [CascadingParameter] ISnackbar Snackbar { get; set; }
        //[Inject] IDbContextFactory<PizzaContext> PizzaContext { get; set; }
        [Inject] HttpContext HttpContext { get; set; }
        [Inject] NavigationManager Navigation { get; set; }

        bool BtnLogin { get; set; }
        bool IsLoading { get; set; } = true;
        LoginModel Model { get; set; } = new();
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

        public async void Login()
        {
            BtnLogin = true;

            //using var context = PizzaContext.CreateDbContext();
            //using var repository = new PizzaRepository(context);

            //var dbUser = repository.Users.GetUserByEmail(Model.Email);

            //// validate dbUser is found and password is correct
            //if (dbUser != null && PasswordHelper.ValidatePassword(Model.Password, dbUser.Password))
            //{
            //    Snackbar.Add($"Logged in successful! Welcome back {dbUser.FirstName}");

            //    // todo - add session cookie
            //    ClaimsIdentity claimsIdentity = new(new List<Claim>
            //    {
            //         new Claim(ClaimTypes.NameIdentifier, dbUser.FirstName)
            //     }, "auth");

            //    ClaimsPrincipal claims = new(claimsIdentity);
            //    await HttpContext.SignInAsync(claims);
            //    Navigation.NavigateTo("/");
            //}
            //else
            //{
            //    Snackbar.Add("Invalid credentials. Please try again", Severity.Error);

            //    // todo - login attempts
            //}

            await Task.Delay(1000);

            BtnLogin = false;
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
    }
}
