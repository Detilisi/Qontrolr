using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Qontrolr.Client.Views;

namespace Qontrolr.Client;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseBarcodeReader()
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

        //Register Services
        builder.Services.AddSingleton<ClientSocketService>();

        // Register ViewModels
        builder.Services.AddSingleton<KeyBoardViewModel>();
        builder.Services.AddSingleton<TouchpadViewModel>();
        builder.Services.AddSingleton<MediaKeysViewModel>();

        // Register Views
        builder.Services.AddSingleton<KeyPadView>();
        builder.Services.AddSingleton<MediaPadView>();
        builder.Services.AddSingleton<TouchpadView>();

        // Register Pages
        builder.Services.AddSingleton<MainPage>();

        return builder.Build();
    }
}
