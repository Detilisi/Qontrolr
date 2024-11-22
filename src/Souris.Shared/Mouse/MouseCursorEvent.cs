namespace Souris.Shared.Mouse;

public static class MouseCursorEvent
{
    //Labels
    public static class Labels
    {
        public const string MoveBy = nameof(MoveBy);
    }

    //Values
    public static class Values
    {
        public record Displacement(int DeltaX, int DeltaY)
        {
            public int DeltaX { get; } = DeltaX;
            public int DeltaY { get; } = DeltaY;
        }
    }
}
