using MudBlazor;
using PizzaApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace PizzaApp.Services
{
    public class UserService
    {
        private readonly APIService _apiService;
        private readonly CookieService _cookieService;

        public UserService(APIService apiService, CookieService cookieService)
        {
            _apiService = apiService;
            _cookieService = cookieService;
        }

        private AuthUser user = new();
        public AuthUser User
        {
            get => user;
            set
            {
                user = value;
                NotifyStateChanged();
            }
        }

        public event Action? OnChange;
        private void NotifyStateChanged() => OnChange?.Invoke();

        public async Task FetchUser()
        {
            string token = await _cookieService.GetCookie("pizza_app_session");
            if (!string.IsNullOrEmpty(token))
            {
                // fetch and fill user
                var response = await _apiService.Get<ValidResponse<AuthUser>>("/api/auth/fetch-user");
                if (response.IsValid)
                {
                    User = response.Data;
                }
            }
        }
    }
}
