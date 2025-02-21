using System.Diagnostics;

namespace Qontrolr.Client.ViewModels.KeyPad;

public partial class KeyPadViewModel : ObservableObject
{
    [RelayCommand]
    public void HandleClickedKey(string key)
    {
        Debug.WriteLine(key);
    }
}
