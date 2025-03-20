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
                if (jsonNode["EventData"]?.Deserialize<CursorVector>() is CursorVector cursorMovedData)
                {
                    InputSimulator.Mouse.MoveMouseBy(cursorMovedData.PosX, cursorMovedData.PosY);
                }
                break;

            case nameof(TouchpadEventNames.WheelScrolled):
                const int scrollFactor = 2;
                if (jsonNode["EventData"]?.Deserialize<ScrollDirection>() is ScrollDirection direction)
                {
                    int scrollValue = direction == ScrollDirection.Up ? scrollFactor : -scrollFactor;
                    InputSimulator.Mouse.VerticalScroll(scrollValue);
                }
                break;

            case nameof(EventNames.ButtonClicked):
            case nameof(EventNames.ButtonPressed):
            case nameof(EventNames.ButtonReleased):
                if (jsonNode["EventData"]?.Deserialize<MouseButtonId>() is MouseButtonId buttonId)
                {
                    HandleMouseButtonAction(eventName, buttonId);
                }
                break;
        }
    }

    private void HandleMouseButtonAction(string eventName, MouseButtonId buttonId)
    {
        switch (buttonId)
        {
            case MouseButtonId.Left:
                PerformLeftButtonAction(eventName);
                break;
            case MouseButtonId.Right:
                PerformRightButtonAction(eventName);
                break;
            case MouseButtonId.Middle:
                PerformMiddleButtonAction(eventName);
                break;
        }
    }

    private void PerformLeftButtonAction(string eventName)
    {
        switch (eventName)
        {
            case nameof(EventNames.ButtonClicked):
                InputSimulator.Mouse.LeftButtonClick();
                break;
            case nameof(EventNames.ButtonPressed):
                InputSimulator.Mouse.LeftButtonDown();
                break;
            case nameof(EventNames.ButtonReleased):
                InputSimulator.Mouse.LeftButtonUp();
                break;
        }
    }

    private void PerformRightButtonAction(string eventName)
    {
        switch (eventName)
        {
            case nameof(EventNames.ButtonClicked):
                InputSimulator.Mouse.RightButtonClick();
                break;
            case nameof(EventNames.ButtonPressed):
                InputSimulator.Mouse.RightButtonDown();
                break;
            case nameof(EventNames.ButtonReleased):
                InputSimulator.Mouse.RightButtonUp();
                break;
        }
    }

    private void PerformMiddleButtonAction(string eventName)
    {
        switch (eventName)
        {
            case nameof(EventNames.ButtonClicked):
                InputSimulator.Mouse.XButtonClick(((int)MouseButton.MiddleButton));
                break;
            case nameof(EventNames.ButtonPressed):
                InputSimulator.Mouse.XButtonDown(((int)MouseButton.MiddleButton));
                break;
            case nameof(EventNames.ButtonReleased):
                InputSimulator.Mouse.XButtonUp(((int)MouseButton.MiddleButton));
                break;
        }
    }
}
