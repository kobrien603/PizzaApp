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
        public UserService()
        {

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
    }
}
