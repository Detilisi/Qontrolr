using Qontrolr.SharedLib.MediaKeys.EventData;

namespace Qontrolr.Client.ViewModels.MediaKeys;

public partial class MediaKeysViewModel(WebSocketService webSocketService) : ViewModel(webSocketService)
{
    [RelayCommand]
    public async Task TogglePlay()
    {
        await _webSocketService.SendDeviceEventAsync(EventNames.ButtonClicked, MediaButtons.Play);
    }

    [RelayCommand]
    public async Task Next()
    {
        await _webSocketService.SendDeviceEventAsync(EventNames.ButtonClicked, MediaButtons.Next);
    }

    [RelayCommand]
    public async Task Previous()
    {
        await _webSocketService.SendDeviceEventAsync(EventNames.ButtonClicked, MediaButtons.Prev);
    }

    [RelayCommand]
    public async Task VolumnUp()
    {
        await _webSocketService.SendDeviceEventAsync(EventNames.ButtonClicked, MediaButtons.VolumnUp);
    }

    [RelayCommand]
    public async Task VolumnDown()
    {
        await _webSocketService.SendDeviceEventAsync(EventNames.ButtonClicked, MediaButtons.VolumnDown);
    }
}
