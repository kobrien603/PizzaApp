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
    public partial class SessionContainer
    {
        [Parameter] public RenderFragment ChildContent { get; set; }

        [Inject] HttpClient Http { get; set; }
        [Inject] public ISnackbar Snackbar { get; set; }
        [Inject] public CookieService CookieService { get; set; }

        public MudTheme Theme { get; set; } = new();
        public bool IsDarkMode { get; set; } = true;
        AuthUser User { get; set; } = new();
        bool IsLoading { get; set; } = true;

        protected override async Task OnInitializedAsync()
        {
            await ManageSession();
            
            IsLoading = false;
            StateHasChanged();
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
    }
}
