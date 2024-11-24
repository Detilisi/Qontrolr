namespace Qontrolr.Shared.Mouse.Button.Events;

public class ButtonPressed : DeviceEvent<ButtonId>
{
    public ButtonPressed(ButtonId data) : base("mouse.button.pressed", data)
    {
    }
}
