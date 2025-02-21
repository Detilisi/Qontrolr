using System.Diagnostics;

namespace Qontrolr.Client.ViewModels.MediaPad;

public partial class MediaPadViewModel : ObservableObject
{
    [RelayCommand]
    public void TogglePlay()
    {
        Debug.WriteLine("Pause");
    }

    [RelayCommand]
    public void Next()
    {
        Debug.WriteLine("Next");
    }

    [RelayCommand]
    public void Previous()
    {
        Debug.WriteLine("Previous");
    }

    [RelayCommand]
    public void VolumnUp()
    {
        Debug.WriteLine("VolumnUp");
    }

    [RelayCommand]
    public void VolumnDown()
    {
        Debug.WriteLine("VolumnDown");
    }
}
