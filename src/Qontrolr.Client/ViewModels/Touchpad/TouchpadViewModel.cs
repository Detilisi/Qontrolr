namespace Qontrolr.Client.ViewModels.TouchPad;

public partial class TouchpadViewModel(WebSocketService webSocketService) : ViewModel(webSocketService)
{
    //Commands
    [RelayCommand]
    public async Task ClickMouseButton(MouseButtonId buttonId)
    {
        await _webSocketService.SendDeviceEventAsync(DeviceId.Touchpad, EventNames.ButtonClicked, buttonId);
    }

    [RelayCommand]
    public async Task PressMouseButton(MouseButtonId buttonId)
    {
        await _webSocketService.SendDeviceEventAsync(DeviceId.Touchpad, EventNames.ButtonPressed, buttonId);
    }

    [RelayCommand]
    public async Task ReleaseMouseButton(MouseButtonId buttonId)
    {
        await _webSocketService.SendDeviceEventAsync(DeviceId.Touchpad, EventNames.ButtonReleased, buttonId);
    }

    [RelayCommand]
    public async Task ScrollMouseWheel(ScrollDirection scrollDirection)
    {
        await _webSocketService.SendDeviceEventAsync(DeviceId.Touchpad, TouchpadEventNames.WheelScrolled, scrollDirection);
    }

    [RelayCommand]
    public async Task DragMousePointer(Vector2 cursorPosition)
    {
        await _webSocketService.SendDeviceEventAsync(DeviceId.Touchpad, TouchpadEventNames.CursorMoved, cursorPosition);
    }
}
