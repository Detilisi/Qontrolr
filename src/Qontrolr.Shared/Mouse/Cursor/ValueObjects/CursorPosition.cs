namespace Qontrolr.Shared.Mouse.Cursor.ValueObjects;

public class CursorPosition(int deltaX, int deltaY)
{
    public int DeltaX { get; } = deltaX;
    public int DeltaY { get; } = deltaY;
}