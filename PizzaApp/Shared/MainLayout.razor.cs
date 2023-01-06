using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaApp
{
    public partial class MainLayout
    {
        bool MenuOpened = false;
        
        [Inject] ISnackbar Snackbar { get; set; }

        void DrawerToggle()
        {
            MenuOpened = !MenuOpened;
        }
    }
}
