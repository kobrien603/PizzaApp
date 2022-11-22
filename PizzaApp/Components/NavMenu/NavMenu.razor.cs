using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaApp.Components
{
    public partial class NavMenu
    {
        [CascadingParameter] ISnackbar Snackbar { get; set; }

        void ShowSnackbarMessage()
        {
            Snackbar.Add("Snackbar message", Severity.Info);
        }
    }
}
