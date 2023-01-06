﻿using Microsoft.AspNetCore.Components;
using MudBlazor;
using PizzaApp.Models;
using PizzaApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PizzaApp.Components
{
    public partial class CreateForm
    {
		[Inject] HttpClient Http { get; set; }
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

			var request = await Http.PostAsJsonAsync("api/users/create-user", NewUser);
            var response = await request.Content.ReadFromJsonAsync<ValidResponse>();
            
            Snackbar.Add(
                response.ResponseMessage, 
                response.IsValid ? Severity.Success : Severity.Error
            );

            if (response.IsValid)
            {
                // navigate
            }

            await Task.Delay(1000);

            BtnCreateAccount = false;
            StateHasChanged();
        }
    }
}