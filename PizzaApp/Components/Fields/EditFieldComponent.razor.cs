using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PizzaApp.Components
{
    public partial class EditFieldComponent<TItem>
    {
        [Inject] public ISnackbar Snackbar { get; set; }

        [Parameter] public string Name { get; set; }
        [Parameter] public TItem Value { get; set; }
        [Parameter] public bool Disabled { get; set; }
        [Parameter] public EventCallback<TItem> ValueChanged { get; set; }

        private bool EditMode = false;
        private bool BtnEdit = false;

        private async Task EditRow()
        {
            BtnEdit = true;

            await Task.Delay(10);

            EditMode = true;
            BtnEdit = false;
        }

        private void CloseEdit()
        {
            BtnEdit = false;
            EditMode = false;
        }

        private Task ConfirmChangesAndClose()
        {
            BtnEdit = false;
            EditMode = false;

            return ValueChanged.InvokeAsync(Value);
        }

        private void OnEnterConfirmChangesAndClose(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
            {
                ConfirmChangesAndClose();
            }
            else if (e.Code == "Escape")
            {
                CloseEdit();
            }
        }
    }
}
