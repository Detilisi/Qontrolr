using System.Diagnostics;

namespace Qontrolr.Client.ViewModels.KeyBoard;

public partial class KeyBoardViewModel(WebSocketService webSocketService) : ViewModel(webSocketService)
{
    [RelayCommand]
    public void HandleClickedKey(string key)
    {
        Debug.WriteLine(key);
    }
}
