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
    public partial class SessionContainer: IDisposable
    {
        [Parameter] public RenderFragment ChildContent { get; set; }

        [Inject] HttpClient Http { get; set; }
        [Inject] public ISnackbar Snackbar { get; set; }
        [Inject] public CookieService CookieService { get; set; }
        [Inject] public ThemeService ThemeService { get; set; }

        AuthUser User { get; set; } = new();
        bool IsLoading { get; set; } = true;

        protected override async Task OnInitializedAsync()
        {
            await ManageSession();
            
            IsLoading = false;
            StateHasChanged();
        }

        protected override void OnInitialized()
        {
            ThemeService.OnChange += StateHasChanged;
        }

        public void Dispose()
        {
            ThemeService.OnChange -= StateHasChanged;
        }

        private async Task ManageSession()
        {
            //string token = await CookieService.GetCookie("pizza_app_session");
            //if (!string.IsNullOrEmpty(token))
            //{
            //    // fetch and fill
            //    await FetchUser();
            //}
        }

        private async Task FetchUser()
        {
            var request = await Http.GetAsync("api/users/get-user");
            var response = await request.Content.ReadFromJsonAsync<ValidResponse<AuthUser>>();
            if (response.IsValid)
            {
                User = response.Data;
            }
        }

        /// <summary>
        /// toggle dark mode on/off
        /// </summary>
        /// <param name="darkMode"></param>
        /// <returns></returns>
        public async Task ToggleDarkMode(bool darkMode)
        {
            ThemeService.IsDarkMode = darkMode;
        }
    }
}
