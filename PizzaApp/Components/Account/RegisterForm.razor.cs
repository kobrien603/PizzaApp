using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using PizzaApp.Models;
using PizzaApp.Services;
using PizzaApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PizzaApp.Components
{
    public partial class RegisterForm
    {
		[Inject] HttpClient Http { get; set; }
        [Inject] CookieService CookieService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] AuthenticationStateProvider AuthStateProvider { get; set; }
        [CascadingParameter] ISnackbar Snackbar { get; set; }

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

        private async Task CreateAccountAsync()
        {
            BtnCreateAccount = true;

			var request = await Http.PostAsJsonAsync("api/auth/register", NewUser);
            var response = await request.Content.ReadFromJsonAsync<ValidResponse>();
            
            Snackbar.Add(
                response.ResponseMessage, 
                response.IsValid ? Severity.Success : Severity.Error
            );

            if (response.IsValid)
            {
                await CookieService.SetCookie("pizza_app_session", response.ResponseMessage, 7);
                await AuthStateProvider.GetAuthenticationStateAsync();

                NavigationManager.NavigateTo("/");
            }

            await Task.Delay(1000);

            BtnCreateAccount = false;
            StateHasChanged();
        }
    }
}
