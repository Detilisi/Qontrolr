using Qontrolr.Shared.Mouse.Button.Enums;
using Qontrolr.Shared.Mouse.Cursor.ValueObjects;
using Qontrolr.Shared.Mouse.Wheel.Enums;
using System.Diagnostics;

namespace Qontrolr.Client.ViewModels.MousePad;

public partial class MousePadViewModel(WebSocketService webSocketService) : ViewModel(webSocketService)
{
    //Commands
    [RelayCommand]
    public async Task ScrollMouseWheel(ScrollDirection scrollDirection)
    {
        Debug.WriteLine(scrollDirection);
        await _webSocketService.SendAsync("MousePad.ScrollMouseWheel");
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
