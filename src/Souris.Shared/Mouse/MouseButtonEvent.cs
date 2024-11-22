namespace Souris.Shared.Mouse;

public static class MouseButtonEvent
{
    //Labels
    public static class Labels
    {
        public const string Click = nameof(Click);
        public const string ClickUp = nameof(ClickUp);
        public const string ClickDown = nameof(ClickDown);
        public const string DoubleClick = nameof(DoubleClick);
    }

    //Values
    public static class Values
    {
        public enum ButtonId
        {
            Left,
            Middle,
            Right,
        }
    }
}
