using Qontrolr.Shared.Common.Events;
using Qontrolr.Shared.Mouse.Button.Constants;
using Qontrolr.Shared.Mouse.Button.Enums;
using Qontrolr.Shared.Mouse.Cursor.Constants;
using Qontrolr.Shared.Mouse.Cursor.ValueObjects;
using Qontrolr.Shared.Mouse.Wheel.Constants;
using Qontrolr.Shared.Mouse.Wheel.Enums;

namespace Qontrolr.Client.ViewModels.MousePad;

public partial class MousePadViewModel(WebSocketService webSocketService) : ViewModel(webSocketService)
{
    //Commands
    [RelayCommand]
    public async Task ScrollMouseWheel(ScrollDirection scrollDirection)
    {
        await _webSocketService.SendEventAsync(new DeviceEvent<ScrollDirection>(WheelEvents.WheelScrolled, scrollDirection));
    }

    [RelayCommand]
    public async Task DragMousePointer(CursorPosition cursorPosition)
    {
        await _webSocketService.SendEventAsync(new DeviceEvent<CursorPosition>(CursorEvents.CursorMoved, cursorPosition));
    }

    [RelayCommand]
    public async Task ClickMouseButton(ButtonId buttonId)
    {
        await _webSocketService.SendEventAsync(new DeviceEvent<ButtonId>(ButtonEvents.ButtonClick, buttonId));
    }
    
    [RelayCommand]
    public async Task PressMouseButton(ButtonId buttonId)
    {
        await _webSocketService.SendEventAsync(new DeviceEvent<ButtonId>(ButtonEvents.ButtonPressed, buttonId));
    }

    [RelayCommand]
    public async Task ReleaseMouseButton(ButtonId buttonId)
    {
        await _webSocketService.SendEventAsync(new DeviceEvent<ButtonId>(ButtonEvents.ButtonReleased, buttonId));
    }
}
