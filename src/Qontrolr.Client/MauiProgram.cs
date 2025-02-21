using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace Qontrolr.Client;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseMauiCommunityToolkitMarkup()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("MaterialIconsRound-Regular.otf", MaterialIconsRound.FontFamily);
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif

        // Register ViewModels
        builder.Services.AddSingleton<KeyPadViewModel>();
        builder.Services.AddSingleton<MousePadViewModel>();
        builder.Services.AddSingleton<MediaPadViewModel>();

        // Register Views
        builder.Services.AddSingleton<KeyPadView>();
        builder.Services.AddSingleton<MediaPadView>();
        builder.Services.AddSingleton<MousePadView>();

        // Register Pages
        builder.Services.AddSingleton<MainPage>();


        return builder.Build();
    }
}
