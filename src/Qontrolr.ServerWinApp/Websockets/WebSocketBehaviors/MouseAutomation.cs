using Qontrolr.Shared.Common.Events;
using Qontrolr.Shared.Mouse.Button.Constants;
using Qontrolr.Shared.Mouse.Button.Enums;
using Qontrolr.Shared.Mouse.Cursor.Constants;
using Qontrolr.Shared.Mouse.Cursor.ValueObjects;
using Qontrolr.Shared.Mouse.Wheel.Constants;
using Qontrolr.Shared.Mouse.Wheel.Enums;
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
        if (jsonMessage.Contains(CursorEvents.CursorMoved))
        {
            var cursorMovedEvent = JsonSerializer.Deserialize<DeviceEvent<CursorPosition>>(jsonMessage);
            if (cursorMovedEvent == null) return;
            
            _inputSimulator.Mouse.MoveMouseBy(cursorMovedEvent.Data.DeltaX, cursorMovedEvent.Data.DeltaY);
        }
        else if (jsonMessage.Contains(ButtonEvents.ButtonClick))
        {
            var clickedEvent = JsonSerializer.Deserialize<DeviceEvent<ButtonId>>(jsonMessage);
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
        else if (jsonMessage.Contains(WheelEvents.WheelScrolled))
        {
            var wheelScrolledEvent = JsonSerializer.Deserialize<DeviceEvent<ScrollDirection>>(jsonMessage);
            if (wheelScrolledEvent == null) return;

            const int scrollFactor = 2;
            if (wheelScrolledEvent.Data == ScrollDirection.Up)
            {
                _inputSimulator.Mouse.VerticalScroll(scrollFactor);
            }
            else if (wheelScrolledEvent.Data == ScrollDirection.Down)
            {
                _inputSimulator.Mouse.VerticalScroll(scrollFactor * -1);
            }
        }
    }    
}
