namespace Qontrolr.Client.ViewModels.SubViewModels;

public partial class TouchpadViewModel(MainViewModel parentViewModel) : SubViewModel(parentViewModel)
{
    //Commands
    [RelayCommand]
    public async Task ClickMouseButton(MouseButtonId buttonId)
    {
        await SendDeviceEventAsync(DeviceId.Touchpad, EventNames.ButtonClicked, buttonId);
    }

    [RelayCommand]
    public async Task PressMouseButton(MouseButtonId buttonId)
    {
        await SendDeviceEventAsync(DeviceId.Touchpad, EventNames.ButtonPressed, buttonId);
    }

    [RelayCommand]
    public async Task ReleaseMouseButton(MouseButtonId buttonId)
    {
        await SendDeviceEventAsync(DeviceId.Touchpad, EventNames.ButtonReleased, buttonId);
    }

    [RelayCommand]
    public async Task ScrollMouseWheel(ScrollDirection scrollDirection)
    {
        await SendDeviceEventAsync(DeviceId.Touchpad, TouchpadEventNames.WheelScrolled, scrollDirection);
    }

    [RelayCommand]
    public async Task DragMousePointer(Vector2 cursorPosition)
    {
        await SendDeviceEventAsync(DeviceId.Touchpad, TouchpadEventNames.CursorMoved, cursorPosition);
    }
}
