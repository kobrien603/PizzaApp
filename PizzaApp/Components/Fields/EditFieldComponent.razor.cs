using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaApp.Components
{
    public partial class EditFieldComponent<TItem>
    {
        [Inject] public ISnackbar Snackbar { get; set; }

        [Parameter] public string Name { get; set; }
        [Parameter] public TItem Value { get; set; }
        [Parameter] public EventCallback<TItem> UpdateValue { get; set; }
    }
}
