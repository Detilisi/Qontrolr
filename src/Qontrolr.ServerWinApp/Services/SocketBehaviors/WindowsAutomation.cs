using Qontrolr.SharedLib;
using Qontrolr.SharedLib.Common;
using Qontrolr.SharedLib.Touchpad;
using Qontrolr.SharedLib.Touchpad.EventData;
using System.Numerics;
using System.Text.Json;
using WebSocketSharp;
using WebSocketSharp.Server;
using WindowsInput;

namespace Qontrolr.Server.Services.SocketBehaviors;

internal class WindowsAutomation : WebSocketBehavior
{
    //Construct
    public static string Endpoint => "qontrolr";
    private readonly InputSimulator _inputSimulator;
    public WindowsAutomation() => _inputSimulator = new InputSimulator();
    
    protected override void OnMessage(MessageEventArgs e)
    {
        var message = e.Data;
        if (string.IsNullOrEmpty(message)) return;
        
        ProccessEvent(message);
    }

    private void ProccessEvent(string jsonMessage)
    {
        var eventRequest = JsonSerializer.Deserialize<DeviceEvent<object>>(jsonMessage);
        if (eventRequest == null) return;

        switch (eventRequest.Device)
        {
            case DeviceId.Touchpad:
                HandleTouchPadRequest(eventRequest);
                break;
            case DeviceId.MediaKeys:
                HandleMediaKeysRequest(eventRequest);
                break;
            case DeviceId.Keyboard:
                HandleKeyboardRequest(eventRequest);
                break;
            default:
                break;
        }
    }


    private void HandleTouchPadRequest(DeviceEvent<object> deviceEvent)
    {
        if (deviceEvent.EventName == TouchpadEventNames.CursorMoved)
        {
            var eventData = (Vector2)deviceEvent.EventData;
            _inputSimulator.Mouse.MoveMouseBy((int)eventData.X, (int)eventData.Y);
        }
        else if (deviceEvent.EventName == TouchpadEventNames.WheelScrolled)
        {
            var eventData = (ScrollDirection)deviceEvent.EventData;
            const int scrollFactor = 2;
            if (eventData == ScrollDirection.Up)
            {
                _inputSimulator.Mouse.VerticalScroll(scrollFactor);
            }
            else if (eventData == ScrollDirection.Down)
            {
                _inputSimulator.Mouse.VerticalScroll(scrollFactor * -1);
            }
        }
        else if (deviceEvent.EventName == EventNames.ButtonClicked)
        {
            var eventData = (MouseButtonId)deviceEvent.EventData;
            if (eventData == MouseButtonId.Right)
            {
                _inputSimulator.Mouse.RightButtonClick();
            }
            else if (eventData == MouseButtonId.Left)
            {
                _inputSimulator.Mouse.LeftButtonClick();
            }
            else if (eventData == MouseButtonId.Middle)
            {
                _inputSimulator.Mouse.XButtonClick((int)MouseButton.MiddleButton);
            }
        }
        else if (deviceEvent.EventName == EventNames.ButtonPressed)
        {
            var eventData = (MouseButtonId)deviceEvent.EventData;
            if (eventData == MouseButtonId.Right)
            {
                _inputSimulator.Mouse.RightButtonDown();
            }
            else if (eventData == MouseButtonId.Left)
            {
                _inputSimulator.Mouse.LeftButtonDown();
            }
            else if (eventData == MouseButtonId.Middle)
            {
                _inputSimulator.Mouse.XButtonDown((int)MouseButton.MiddleButton);
            }
        }
        else if (deviceEvent.EventName == EventNames.ButtonReleased)
        {
            var eventData = (MouseButtonId)deviceEvent.EventData;
            if (eventData == MouseButtonId.Right)
            {
                _inputSimulator.Mouse.RightButtonUp();
            }
            else if (eventData == MouseButtonId.Left)
            {
                _inputSimulator.Mouse.LeftButtonUp();
            }
            else if (eventData == MouseButtonId.Middle)
            {
                _inputSimulator.Mouse.XButtonUp((int)MouseButton.MiddleButton);
            }
        }
    }

    private void HandleMediaKeysRequest(DeviceEvent<object> deviceEvent)
    {

    }

    private void HandleKeyboardRequest(DeviceEvent<object> deviceEvent)
    {

    }

}

