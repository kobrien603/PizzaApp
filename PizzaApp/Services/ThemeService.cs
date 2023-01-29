using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaApp.Services
{
    public class ThemeService
    {
        public MudTheme Theme { get; set; } = new();
        public bool IsDarkMode { get; set; } = true;

        public event Action<bool> ToggleDarkMode;
    }
}
