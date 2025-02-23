using System.Diagnostics;

namespace Qontrolr.Client.ViewModels.KeyPad;

public partial class KeyPadViewModel(WebSocketService webSocketService) : ViewModel(webSocketService)
{
    [RelayCommand]
    public void HandleClickedKey(string key)
    {
        Debug.WriteLine(key);
    }
}
