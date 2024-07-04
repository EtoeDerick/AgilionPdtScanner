using AgilionPdtScanner.Repositories;
using AgilionPdtScanner.ViewModels;
using AgilionPdtScanner.Views;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using ZXing.Net.Maui.Controls;

namespace AgilionPdtScanner
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseBarcodeReader()
                
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                }); 

            builder.Services.AddSingleton<IReadRepository, MockDataRepository>();
            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddTransient<OrderDetailsPageViewModel>();
            builder.Services.AddSingleton<MainPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif
            //ADDED
            var app = builder.Build();

            // Manually create and set the App with service provider
            var appInstance = app.Services.GetService<App>();
            Application.Current = appInstance;

            //return builder.Build();
            return app;
        }

    }
}
