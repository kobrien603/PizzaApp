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
    public partial class MainLayout
    {
        [Inject] private NavigationManager NavigationManager { get; set; }

        [Inject] ThemeService ThemeService { get; set; }

        bool MenuOpened { get; set; }

        void DrawerToggle()
        {
            MenuOpened = !MenuOpened;
        }
    }
}
