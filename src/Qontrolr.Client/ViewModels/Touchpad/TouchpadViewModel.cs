using Qontrolr.Shared.Mouse.Button.Constants;
using Qontrolr.Shared.Mouse.Button.Enums;
using Qontrolr.Shared.Mouse.Cursor.Constants;
using Qontrolr.Shared.Mouse.Cursor.ValueObjects;
using Qontrolr.Shared.Mouse.Wheel.Constants;
using Qontrolr.Shared.Mouse.Wheel.Enums;

namespace Qontrolr.Client.ViewModels.TouchPad;

public partial class TouchpadViewModel(WebSocketService webSocketService) : ViewModel(webSocketService)
{
    //Commands
    [RelayCommand]
    public async Task ClickMouseButton(ButtonId buttonId)
    {
        await _webSocketService.SendDeviceEventAsync(ButtonEvents.ButtonClick, buttonId);
    }

    [RelayCommand]
    public async Task PressMouseButton(ButtonId buttonId)
    {
        await _webSocketService.SendDeviceEventAsync(ButtonEvents.ButtonPressed, buttonId);
    }

    [RelayCommand]
    public async Task ReleaseMouseButton(ButtonId buttonId)
    {
        await _webSocketService.SendDeviceEventAsync(ButtonEvents.ButtonReleased, buttonId);
    }

    [RelayCommand]
    public async Task ScrollMouseWheel(ScrollDirection scrollDirection)
    {
        await _webSocketService.SendDeviceEventAsync(WheelEvents.WheelScrolled, scrollDirection);
    }

    [RelayCommand]
    public async Task DragMousePointer(CursorPosition cursorPosition)
    {
        await _webSocketService.SendDeviceEventAsync(CursorEvents.CursorMoved, cursorPosition);
    }
}
