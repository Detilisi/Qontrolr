namespace Qontrolr.Shared.Mouse.Button.Events;

public class ButtonPressed : DeviceEvent<ButtonId>
{
    public ButtonPressed(ButtonId data) : base("button.pressed", data)
    {
    }
}
