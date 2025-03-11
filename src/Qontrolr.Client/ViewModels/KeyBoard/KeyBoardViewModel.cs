namespace Qontrolr.Client.ViewModels.KeyBoard;

public partial class KeyBoardViewModel(ClientSocketService webSocketService) : ViewModel(webSocketService)
{
    [RelayCommand]
    public async Task HandleClickedKey(string key)
    {
        await _webSocketService.SendDeviceEventAsync(DeviceId.Keyboard, EventNames.ButtonClicked, key);
    }

    [RelayCommand]
    public async Task HandleClickedWinButton(WinButtonId winButton)
    {

        await _webSocketService.SendDeviceEventAsync(DeviceId.Keyboard, EventNames.ButtonClicked, winButton);
    }
}
