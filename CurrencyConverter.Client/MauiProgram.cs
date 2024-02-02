using CommunityToolkit.Maui;
using CurrencyConverter.Client.ViewModels;
using CurrencyConverter.Shared.Services;
using Microsoft.Extensions.Logging;
using Refit;

namespace CurrencyConverter.Client
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.UseMauiCommunityToolkit();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainViewModel>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddRefitClient<ICurrencyConverterService>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7121"));

            return builder.Build();
        }
    }
}
