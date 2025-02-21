using Qontrolr.Shared.Mouse.Button.Enums;
using Qontrolr.Shared.Mouse.Cursor.ValueObjects;
using Qontrolr.Shared.Mouse.Wheel.Enums;
using System.Diagnostics;

namespace Qontrolr.Client.ViewModels.MousePad;

public partial class MousePadViewModel : ObservableObject
{
    //Commands
    [RelayCommand]
    public void ScrollMouseWheel(ScrollDirection scrollDirection)
    {
        Debug.WriteLine(scrollDirection);
    }

    [RelayCommand]
    public void DragMousePointer(CursorPosition cursorPosition)
    {
        Debug.WriteLine(cursorPosition.PosX + "," + cursorPosition.PosY);
    }

    [RelayCommand]
    public void ClickMouseButton(ButtonId buttonId)
    {
        Debug.WriteLine(buttonId);
    }
    
    [RelayCommand]
    public void PressMouseButton(ButtonId buttonId)
    {
        Debug.WriteLine(buttonId);
    }

    [RelayCommand]
    public void ReleaseMouseButton(ButtonId buttonId)
    {
        Debug.WriteLine(buttonId);
    }
}
