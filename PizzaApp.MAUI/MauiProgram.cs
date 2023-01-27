using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebView.Maui;
using PizzaApp.Provider;

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

            string url = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5000" : "http://local.pizza.com";
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(url) });

            return builder.Build();
        }
    }
}