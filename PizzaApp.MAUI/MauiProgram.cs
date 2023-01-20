﻿using Microsoft.AspNetCore.Components.WebView.Maui;

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
			builder.Services.InjectPizzaApp();

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:80/") });

            return builder.Build();
        }
    }
}