using Qontrolr.Shared.Mouse.Wheel.Enums;

namespace Qontrolr.Shared.Mouse.Wheel.Events;

public class WheelScrolled : DeviceEvent<ScrollDirection>
{
    public WheelScrolled(ScrollDirection data) : base("mouse.scroll", data)
    {
    }
}
