using Qontrolr.SharedLib;
using Qontrolr.SharedLib.Common;
using Qontrolr.SharedLib.MediaKeys.EventData;
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
        var jsonMessage = e.Data;
        if (string.IsNullOrEmpty(jsonMessage)) return;

        try
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
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
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
        if (deviceEvent.EventName != EventNames.ButtonClicked) return;

        var eventData = (MediaButtonId)deviceEvent.EventData;
        if (eventData == MediaButtonId.Play)
        {
            _inputSimulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.MEDIA_PLAY_PAUSE);
        }
        else if (eventData == MediaButtonId.Next)
        {
            _inputSimulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.MEDIA_NEXT_TRACK);
        }
        else if (eventData == MediaButtonId.Prev)
        {
            _inputSimulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.MEDIA_PREV_TRACK);
        }
        else if (eventData == MediaButtonId.VolumnUp)
        {
            _inputSimulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.VOLUME_UP);
        }
        else if (eventData == MediaButtonId.VolumnDown)
        {
            _inputSimulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.VOLUME_DOWN);
        }
    }

    private void HandleKeyboardRequest(DeviceEvent<object> deviceEvent)
    {
        if (deviceEvent.EventName != EventNames.ButtonClicked) return;
        var eventData = (string)deviceEvent.EventData;

        _inputSimulator.Keyboard.TextEntry(eventData);
    }
}

