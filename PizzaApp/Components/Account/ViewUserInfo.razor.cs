using Microsoft.AspNetCore.Components;
using MudBlazor;
using PizzaApp.Services;
using PizzaApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaApp.Components
{
    public partial class ViewUserInfo : IDisposable
    {
        [Inject] public ISnackbar Snackbar { get; set; }
        [Inject] public UserService UserService { get; set; }

        bool IsLoading { get; set; } = true;
        AuthUser User { get; set; } = new();

        protected override void OnInitialized()
        {
            User = UserService.User;

            UserService.OnChange += StateHasChanged;
        }

        public void Dispose()
        {
            UserService.OnChange -= StateHasChanged;
            GC.SuppressFinalize(this);
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

        private void SetProfilePicture(string profilePic)
        {
            UserService.User.ProfilePicture = profilePic;
            StateHasChanged();
        }
    }
}
