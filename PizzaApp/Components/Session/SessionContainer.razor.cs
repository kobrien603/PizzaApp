using Microsoft.AspNetCore.Components;
using MudBlazor;
using PizzaApp.Services;
using PizzaApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PizzaApp.Components
{
    public partial class SessionContainer
    {
        [Parameter] public RenderFragment ChildContent { get; set; }

        [Inject] public UserService UserService { get; set; }

        bool IsLoading { get; set; } = true;

        protected override async Task OnInitializedAsync()
        {
            await UserService.FetchUser();

            IsLoading = false;
            StateHasChanged();
        }
    }
}
