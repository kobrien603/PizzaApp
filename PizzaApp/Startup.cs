using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using MudBlazor.Services;
using PizzaApp.Provider;
using PizzaApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaApp
{
    public static class Startup
    {
        /// <summary>
        /// used to help inject DI projects from PizzaApp to any web or app tool that connects to it
        /// ex: in PizzaApp.Web we add builder.Services.TryAddPizzaAppRCL();
        /// </summary>
        public static void InjectPizzaApp(this IServiceCollection services)
        {
            // add mudblazor
            services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;
                config.SnackbarConfiguration.PreventDuplicates = false;
                config.SnackbarConfiguration.NewestOnTop = false;
                config.SnackbarConfiguration.ShowCloseIcon = true;
                config.SnackbarConfiguration.VisibleStateDuration = 10000;
                config.SnackbarConfiguration.HideTransitionDuration = 500;
                config.SnackbarConfiguration.ShowTransitionDuration = 500;
                config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
            });

            services.AddTransient<APIService>();
            services.AddTransient<CookieService>();

            services.AddSingleton<UserService>();
            services.AddSingleton<ThemeService>();

            // custom auth state provider
            services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
            services.AddAuthorizationCore();
        }
    }
}
