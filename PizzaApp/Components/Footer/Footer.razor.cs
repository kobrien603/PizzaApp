using Microsoft.AspNetCore.Components;
using MudBlazor;
using PizzaApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaApp.Components
{
    partial class Footer
    {
        [Inject] public ThemeService ThemeService { get; set; }
    }
}
