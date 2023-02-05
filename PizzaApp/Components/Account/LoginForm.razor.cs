using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using PizzaApp.Shared.Models;
using PizzaApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;

namespace PizzaApp.Components
{
    public partial class LoginForm : IDisposable
    {
        [Inject] ISnackbar Snackbar { get; set; }
        [Inject] APIService APIService { get; set; }
        [Inject] UserService UserService { get; set; }
        [Inject] CookieService CookieService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] AuthenticationStateProvider AuthStateProvider { get; set; }

        bool BtnLogin { get; set; }
        bool IsLoading { get; set; } = true;
        LoginModel Model { get; set; } = new();
        bool ShowPassword { get; set; } = true;
        InputType PasswordInput { get; set; } = InputType.Password;
        string PasswordInputIcon { get; set; } = Icons.Material.Filled.VisibilityOff;

        protected override void OnInitialized()
        {
            UserService.OnChange += StateHasChanged;
        }

        public void Dispose()
        {
            UserService.OnChange -= StateHasChanged;
        }

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

            var request = await APIService.Post("api/auth/login", Model);
            var response = await request.Content.ReadFromJsonAsync<ValidResponse<AuthUser>>();

            //Snackbar.Add(
            //    response.ResponseMessage,
            //    response.IsValid ? Severity.Success : Severity.Error
            //);

            if (response.IsValid)
            {
                UserService.User = response.Data;
                await CookieService.SetCookie("pizza_app_session", response.ResponseMessage, 7);
                await AuthStateProvider.GetAuthenticationStateAsync();

                Snackbar.Add("Login successful!", Severity.Success);

                NavigationManager.NavigateTo("/");
            }
            else
            {
                Snackbar.Add(
                    response.ResponseMessage,
                    Severity.Error
                );
            }

            BtnLogin = false;
            StateHasChanged();
        }

        public async void Test()
        {
            BtnLogin = true;

            var response = await APIService.Get<ValidResponse>("/api/auth/test");

            Snackbar.Add(
               response.ResponseMessage,
               Severity.Error
            );

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
