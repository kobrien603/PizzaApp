using Microsoft.AspNetCore.Components;
using MudBlazor;
using PizzaApp.Components;
using PizzaApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaApp
{
    public partial class MainLayout : IDisposable
    {
        [Inject] public ThemeService ThemeService { get; set; }

        private bool MenuOpened { get; set; }
        private MudThemeProvider MudThemeProvider { get; set; }

        protected override void OnInitialized()
        {
            ThemeService.OnChange += StateHasChanged;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                ThemeService.IsDarkMode = await MudThemeProvider.GetSystemPreference();
            }
        }

        public void Dispose()
        {
            ThemeService.OnChange -= StateHasChanged;
            GC.SuppressFinalize(this);
        }
    }
}
