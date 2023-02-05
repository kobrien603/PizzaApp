using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using PizzaApp.Provider;
using PizzaApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaApp.Components
{
    public partial class UserNavMenu : IDisposable
    {
        [Inject] public ISnackbar Snackbar { get; set; }
        [Inject] public UserService UserService { get; set; }
        [Inject] public ThemeService ThemeService { get; set; }
        [Inject] public CookieService CookieService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public AuthenticationStateProvider AuthStateProvider { get; set; }

        protected override void OnInitialized()
        {
            UserService.OnChange += StateHasChanged;
            ThemeService.OnChange += StateHasChanged;
        }

        public void Dispose()
        {
            UserService.OnChange -= StateHasChanged;
            ThemeService.OnChange -= StateHasChanged;
        }

        /// <summary>
        /// log user out
        /// </summary>
        /// <returns></returns>
        public async Task LogUserOut()
        {
            if (!string.IsNullOrEmpty(await CookieService.GetCookie("pizza_app_session")))
            {
                await CookieService.DeleteCookie("pizza_app_session");
                await AuthStateProvider.GetAuthenticationStateAsync();
                UserService.User = new();

                Snackbar.Add("Logged out successfully", Severity.Normal);
                NavigationManager.NavigateTo("/logout");
            }
        }
    }
}
