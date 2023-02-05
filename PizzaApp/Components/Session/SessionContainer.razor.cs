using Microsoft.AspNetCore.Components;
using MudBlazor;
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
    public partial class SessionContainer : IDisposable
    {
        [Parameter] public RenderFragment ChildContent { get; set; }

        [Inject] public UserService UserService { get; set; }
        [Inject] public APIService APIService { get; set; }
        [Inject] public CookieService CookieService { get; set; }

        bool IsLoading { get; set; } = true;

        protected override void OnInitialized()
        {
            UserService.OnChange += StateHasChanged;
        }

        public void Dispose()
        {
            UserService.OnChange -= StateHasChanged;
        }

        protected override async Task OnInitializedAsync()
        {
            await FetchUser();

            IsLoading = false;
            StateHasChanged();
        }

        /// <summary>
        /// fetch user info and populate UserService
        /// </summary>
        /// <returns></returns>
        public async Task FetchUser()
        {
            string token = await CookieService.GetCookie("pizza_app_session");
            if (!string.IsNullOrEmpty(token))
            {
                // fetch and fill user
                var response = await APIService.Get<ValidResponse<AuthUser>>("/api/auth/fetch-user");
                if (response.IsValid)
                {
                    UserService.User = response.Data;
                }
            }
        }
    }
}
