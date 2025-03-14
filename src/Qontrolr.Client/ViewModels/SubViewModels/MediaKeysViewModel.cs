using Qontrolr.SharedLib.MediaKeys.EventData;

namespace Qontrolr.Client.ViewModels.SubViewModels;

public partial class MediaKeysViewModel(MainViewModel parentViewModel) : SubViewModel(parentViewModel)
{
    [RelayCommand]
    public async Task TogglePlay()
    {
        await SendDeviceEventAsync(DeviceId.MediaKeys, EventNames.ButtonClicked, MediaButtonId.Play);
    }

    [RelayCommand]
    public async Task Next()
    {
        await SendDeviceEventAsync(DeviceId.MediaKeys, EventNames.ButtonClicked, MediaButtonId.Next);
    }

    [RelayCommand]
    public async Task Previous()
    {
        await SendDeviceEventAsync(DeviceId.MediaKeys, EventNames.ButtonClicked, MediaButtonId.Prev);
    }

    [RelayCommand]
    public async Task VolumnUp()
    {
        await SendDeviceEventAsync(DeviceId.MediaKeys, EventNames.ButtonClicked, MediaButtonId.VolumnUp);
    }

    [RelayCommand]
    public async Task VolumnDown()
    {
        await SendDeviceEventAsync(DeviceId.MediaKeys, EventNames.ButtonClicked, MediaButtonId.VolumnDown);
    }
}
