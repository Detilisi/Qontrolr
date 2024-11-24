namespace Qontrolr.Shared.Mouse.Button.Events;

public class ButtonClicked : DeviceEvent<ButtonId>
{
    public ButtonClicked(ButtonId data) : base("mouse.button.clicked", data)
    {
    }
}
