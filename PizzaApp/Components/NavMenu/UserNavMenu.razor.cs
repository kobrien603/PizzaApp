using Microsoft.AspNetCore.Components;
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
        [Inject] public UserService UserService { get; set; }
        [Inject] public ThemeService ThemeService { get; set; }

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
    }
}
