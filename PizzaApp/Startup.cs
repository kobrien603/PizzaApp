using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using MudBlazor.Services;
using PizzaApp.Server.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace PizzaApp
{ 
    public static class Startup
    {
        /// <summary>
        /// used to help inject DI projects from PizzaApp to any web or app tool that connects to it
        /// ex: in PizzaApp.Web we add builder.Services.TryAddPizzaAppRCL();
        /// </summary>
        public static void TryAddPizzaAppRCL(this IServiceCollection services)
        {
            // add database
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddDbContextFactory<PizzaContext>(options =>
                options.UseMySQL()
            );

            // add mudblazor
            services.AddMudBlazorSnackbar();
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
        }
    }
}
