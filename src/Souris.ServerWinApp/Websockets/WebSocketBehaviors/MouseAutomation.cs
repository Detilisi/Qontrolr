using Qontrolr.Shared.Mouse.Button.Enums;
using Qontrolr.Shared.Mouse.Button.Events;
using Qontrolr.Shared.Mouse.Cursor.Events;
using Qontrolr.Shared.Mouse.Wheel.Enums;
using Qontrolr.Shared.Mouse.Wheel.Events;
using System.Text.Json;
using WebSocketSharp;
using WebSocketSharp.Server;
using WindowsInput;

namespace Qontrolr.Server.Websockets.WebSocketBehaviors;

internal class MouseAutomation : WebSocketBehavior
{
    //Fields
    private readonly InputSimulator _inputSimulator;
    public static string Endpoint => "/mouse-automation";

    //Construction
    public MouseAutomation()
    {
        _inputSimulator = new InputSimulator();
    }

    //Event handlers
    protected override void OnMessage(MessageEventArgs e)
    {
        var message = e.Data;
        if (string.IsNullOrEmpty(message)) return;

        ProccessEvent(message);
    }

    //Hepler
    private void ProccessEvent(string jsonMessage)
    {
        if (jsonMessage.Contains("mouse.cursor.moved"))
        {
            var cursorMovedEvent = JsonSerializer.Deserialize<CursorMoved>(jsonMessage);
            if (cursorMovedEvent == null) return;

            _inputSimulator.Mouse.MoveMouseBy(cursorMovedEvent.Data.DeltaX, cursorMovedEvent.Data.DeltaY);
        }
        else if (jsonMessage.Contains("mouse.button.clicked"))
        {
            var clickedEvent = JsonSerializer.Deserialize<ButtonClicked>(jsonMessage);
            if (clickedEvent == null) return;

            if (clickedEvent.Data == ButtonId.Right)
            {
                _inputSimulator.Mouse.RightButtonClick();
            }
            else if (clickedEvent.Data == ButtonId.Left)
            {
                _inputSimulator.Mouse.LeftButtonClick();
            }
        }
        else if (jsonMessage.Contains("mouse.wheel.scrolled"))
        {
            var wheelScrolledEvent = JsonSerializer.Deserialize<WheelScrolled>(jsonMessage);
            if (wheelScrolledEvent == null) return;

            const int scrollFactor = 2;
            if (wheelScrolledEvent.Data == ScrollDirection.Up)
            {
                _inputSimulator.Mouse.VerticalScroll(scrollFactor);
            }
            else if (wheelScrolledEvent.Data == ScrollDirection.Down)
            {
                _inputSimulator.Mouse.VerticalScroll(-scrollFactor);
            }
        }
    }    
}
