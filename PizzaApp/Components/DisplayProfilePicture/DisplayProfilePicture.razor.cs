using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaApp.Components
{
    public partial class DisplayProfilePicture
    {
        [Parameter]
        public string ProfilePicture { get; set; } = string.Empty;

        [Parameter]
        public Size Size { get; set; } = Size.Medium;
    }
}
