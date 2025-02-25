namespace Qontrolr.SharedLib.Common;

public static class EventNames
{
    public static string ButtonClicked => nameof(ButtonClicked);
    public static string ButtonDoubledClicked => nameof(ButtonDoubledClicked);
    public static string ButtonPressed => nameof(ButtonPressed); //Click without release
    public static string ButtonReleased => nameof(ButtonReleased);
}
