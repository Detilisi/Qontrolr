using Qontrolr.SharedLib.MediaKeys.EventData;

namespace Qontrolr.Client.ViewModels.MediaKeys;

public partial class MediaKeysViewModel(WebSocketService webSocketService) : ViewModel(webSocketService)
{
    [RelayCommand]
    public async Task TogglePlay()
    {
        await _webSocketService.SendDeviceEventAsync(DeviceId.MediaKeys, EventNames.ButtonClicked, MediaButtonId.Play);
    }

    [RelayCommand]
    public async Task Next()
    {
        await _webSocketService.SendDeviceEventAsync(DeviceId.MediaKeys, EventNames.ButtonClicked, MediaButtonId.Next);
    }

    [RelayCommand]
    public async Task Previous()
    {
        await _webSocketService.SendDeviceEventAsync(DeviceId.MediaKeys, EventNames.ButtonClicked, MediaButtonId.Prev);
    }

    [RelayCommand]
    public async Task VolumnUp()
    {
        await _webSocketService.SendDeviceEventAsync(DeviceId.MediaKeys, EventNames.ButtonClicked, MediaButtonId.VolumnUp);
    }

    [RelayCommand]
    public async Task VolumnDown()
    {
        await _webSocketService.SendDeviceEventAsync(DeviceId.MediaKeys, EventNames.ButtonClicked, MediaButtonId.VolumnDown);
    }
}
