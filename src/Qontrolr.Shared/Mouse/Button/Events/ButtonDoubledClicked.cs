namespace Qontrolr.Shared.Mouse.Button.Events;

public class ButtonDoubledClicked : DeviceEvent<ButtonId>
{
    public ButtonDoubledClicked(ButtonId data) : base("mouse.button.double.clicked", data)
    {
    }
}