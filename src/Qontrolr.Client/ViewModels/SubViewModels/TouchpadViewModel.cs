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
        await SendDeviceEventAsync(DeviceId.Touchpad, EventNames.ButtonDown, buttonId);
    }

    [RelayCommand]
    public async Task ReleaseMouseButton(MouseButtonId buttonId)
    {
        await SendDeviceEventAsync(DeviceId.Touchpad, EventNames.ButtonUp, buttonId);
    }

    [RelayCommand]
    public async Task ScrollMouseWheel(ScrollDirection scrollDirection)
    {
        await SendDeviceEventAsync(DeviceId.Touchpad, TouchpadEventNames.WheelScrolled, scrollDirection);
    }

    [RelayCommand]
    public async Task MoveCursorRelative(CursorVector cursorPosition)
    {
        await SendDeviceEventAsync(DeviceId.Touchpad, TouchpadEventNames.CursorMoved, cursorPosition);
    }
}
