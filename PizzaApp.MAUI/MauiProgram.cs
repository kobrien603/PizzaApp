using Microsoft.AspNetCore.Components.WebView.Maui;
using Microsoft.Extensions.Configuration;
using PizzaApp.Server;
using PizzaApp.Server.DAL;

namespace PizzaApp.MAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
            
            #if DEBUG
		    builder.Services.AddBlazorWebViewDeveloperTools();
            #endif

			// add DI from PizzaApp
			builder.Services.TryAddPizzaAppRCL();
			builder.Services.TryAndAddPizzaServer();

			return builder.Build();
        }
    }
}