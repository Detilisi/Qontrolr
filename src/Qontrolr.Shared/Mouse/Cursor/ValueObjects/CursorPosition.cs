namespace Qontrolr.Shared.Mouse.Cursor.ValueObjects;

public class CursorPosition(int posX, int posY)
{
    public int PosX { get; } = posX;
    public int PosY { get; } = posY;
}