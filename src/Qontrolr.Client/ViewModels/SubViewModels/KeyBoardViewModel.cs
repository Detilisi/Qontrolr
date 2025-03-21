namespace Qontrolr.Client.ViewModels.SubViewModels;

public partial class KeyBoardViewModel(MainViewModel parentViewModel) : SubViewModel(parentViewModel)
{
    [RelayCommand]
    public async Task HandleClickedKey(string key)
    {
        await SendDeviceEventAsync(DeviceId.Keyboard, EventNames.KeyClicked, key);
    }

    [RelayCommand]
    public async Task HandleClickedWinButton(WinButtonId winButton)
    {
        await SendDeviceEventAsync(DeviceId.Keyboard, EventNames.KeyClicked, winButton);
    }
}
