using Qontrolr.SharedLib.Touchpad;
using Qontrolr.SharedLib.Touchpad.EventData;

namespace Qontrolr.Server.Services.Handlers;

public class TouchPadJsonNodeHandler : JsonNodeHandler
{
    public override void Handle(JsonNode jsonNode)
    {
        string eventName = jsonNode["EventName"]?.ToString() ?? string.Empty;
        if (string.IsNullOrEmpty(eventName)) return;

        switch (eventName)
        {
            case nameof(TouchpadEventNames.CursorMoved):
                var cursorMovedData = jsonNode["EventData"].Deserialize<CursorVector>();
                if (cursorMovedData == null) break;

                InputSimulator.Mouse.MoveMouseBy(cursorMovedData.PosX, cursorMovedData.PosY);
                break;

            case nameof(TouchpadEventNames.WheelScrolled):
                const int scrollFactor = 2;
                var wheelScrolledData = jsonNode["EventData"].Deserialize<ScrollDirection>();
                var scrollValue = wheelScrolledData == ScrollDirection.Up ? scrollFactor : scrollFactor * -1;

                InputSimulator.Mouse.VerticalScroll(scrollValue);
                break;

            case nameof(EventNames.ButtonClicked):
                var buttonClickedData = jsonNode["EventData"].Deserialize<MouseButtonId>();
                var buttonClicked = buttonClickedData switch
                {
                    MouseButtonId.Right => MouseButton.RightButton,
                    MouseButtonId.Left => MouseButton.LeftButton,
                    MouseButtonId.Middle => MouseButton.MiddleButton,
                    _ => MouseButton.MiddleButton
                };

                InputSimulator.Mouse.XButtonClick((int)buttonClicked);
                break;
            case nameof(EventNames.ButtonPressed):
                var buttonPressedData = jsonNode["EventData"].Deserialize<MouseButtonId>();
                var buttonPressed = buttonPressedData switch
                {
                    MouseButtonId.Right => MouseButton.RightButton,
                    MouseButtonId.Left => MouseButton.LeftButton,
                    MouseButtonId.Middle => MouseButton.MiddleButton,
                    _ => MouseButton.MiddleButton
                };

                InputSimulator.Mouse.XButtonDown((int)buttonPressed);
                break;
            case nameof(EventNames.ButtonReleased):
                var buttonReleasedData = jsonNode["EventData"].Deserialize<MouseButtonId>();
                var buttonReleased = buttonReleasedData switch
                {
                    MouseButtonId.Right => MouseButton.RightButton,
                    MouseButtonId.Left => MouseButton.LeftButton,
                    MouseButtonId.Middle => MouseButton.MiddleButton,
                    _ => MouseButton.MiddleButton
                };

                InputSimulator.Mouse.XButtonUp((int)buttonReleased);
                break;
            default:
                break;
        }
    }
}
