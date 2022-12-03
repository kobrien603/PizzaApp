﻿using Microsoft.AspNetCore.Components;
using MudBlazor;
using PizzaApp.Models;
using PizzaApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace PizzaApp.Components
{
    public partial class LoginForm
    {
        [Inject] HttpClient Http { get; set; }
        [Inject] NavigationManager Navigation { get; set; }
        [CascadingParameter] ISnackbar Snackbar { get; set; }

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

            var request = await Http.PostAsJsonAsync("api/login", Model);
            var response = await request.Content.ReadFromJsonAsync<ValidResponse>();

            Snackbar.Add(
                response.ResponseMessage,
                response.IsValid ? Severity.Success : Severity.Error
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
