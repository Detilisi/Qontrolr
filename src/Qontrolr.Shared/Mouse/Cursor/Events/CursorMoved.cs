using Qontrolr.Shared.Common.Base;
using Qontrolr.Shared.Mouse.Cursor.ValueObjects;

namespace Qontrolr.Shared.Mouse.Cursor.Events;

public class CursorMoved : DeviceEvent<CursorPosition>
{
    public CursorMoved(CursorPosition data) : base("cursor.moved", data)
    {
    }
}
