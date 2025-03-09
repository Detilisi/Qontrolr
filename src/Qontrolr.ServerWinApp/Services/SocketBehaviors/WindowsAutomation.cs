using Qontrolr.SharedLib;
using Qontrolr.SharedLib.Common;
using Qontrolr.SharedLib.MediaKeys.EventData;
using Qontrolr.SharedLib.Touchpad;
using Qontrolr.SharedLib.Touchpad.EventData;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Nodes;
using WebSocketSharp;
using WebSocketSharp.Server;
using WindowsInput;

namespace Qontrolr.Server.Services.SocketBehaviors;

internal class WindowsAutomation : WebSocketBehavior
{
    //Construct
    public static string Endpoint => "/qontrolr";
    private readonly InputSimulator _inputSimulator;
    public WindowsAutomation() => _inputSimulator = new InputSimulator();

    override protected void OnOpen()
    {
        Console.WriteLine($"Client connected: {ID}");
        Send("Connected to Windows Automation Server");   
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        var jsonMessage = e.Data;
        if (string.IsNullOrEmpty(jsonMessage)) return;

        try
        {
            JsonNode jsonNode = JsonNode.Parse(jsonMessage);
            if (jsonNode == null) return;

            DeviceId deviceId = jsonNode["Device"].Deserialize<DeviceId>();
            switch (deviceId)
            {
                case DeviceId.Touchpad:

                    HandleTouchPadJsonEvent(jsonNode);
                    break;
                case DeviceId.MediaKeys:
                    HandleMediaKeysJsonEvent(jsonNode);
                    break;
                case DeviceId.Keyboard:
                    HandleKeyboardJsonEvent(jsonNode);
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

    private void HandleTouchPadJsonEvent(JsonNode jsonNode)
    {
        string eventName = jsonNode["EventName"]?.ToString() ?? string.Empty;
        if (string.IsNullOrEmpty(eventName)) return;

        switch (eventName)
        {
            case nameof(TouchpadEventNames.CursorMoved):
                var cursorMovedData = jsonNode["EventData"].Deserialize<Vector2>();
                _inputSimulator.Mouse.MoveMouseBy((int)cursorMovedData.X, (int)cursorMovedData.Y);
                break;

            case nameof(TouchpadEventNames.WheelScrolled):
                const int scrollFactor = 2;
                var wheelScrolledData = jsonNode["EventData"].Deserialize<ScrollDirection>();
                var scrollValue = wheelScrolledData == ScrollDirection.Up ? scrollFactor : scrollFactor * -1;

                _inputSimulator.Mouse.VerticalScroll(scrollValue);
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

                _inputSimulator.Mouse.XButtonClick((int)buttonClicked);
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

                _inputSimulator.Mouse.XButtonDown((int)buttonPressed);
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

                _inputSimulator.Mouse.XButtonUp((int)buttonReleased);
                break;
            default:
                break;
        }
    }

    private void HandleMediaKeysJsonEvent(JsonNode jsonNode)
    {
        string eventName = jsonNode["EventName"]?.ToString() ?? string.Empty;
        if (string.IsNullOrEmpty(eventName)) return;

        switch (eventName)
        {
            case nameof(EventNames.ButtonClicked):
                var mediaButtonClickedData = jsonNode["EventData"].Deserialize<MediaButtonId>();
                var mediaButtonClicked = mediaButtonClickedData switch
                {
                    MediaButtonId.Play => WindowsInput.Native.VirtualKeyCode.MEDIA_PLAY_PAUSE,
                    MediaButtonId.Next => WindowsInput.Native.VirtualKeyCode.MEDIA_NEXT_TRACK,
                    MediaButtonId.Prev => WindowsInput.Native.VirtualKeyCode.MEDIA_PREV_TRACK,
                    MediaButtonId.VolumnUp => WindowsInput.Native.VirtualKeyCode.VOLUME_UP,
                    MediaButtonId.VolumnDown => WindowsInput.Native.VirtualKeyCode.VOLUME_DOWN,
                    //_ => 
                };

                _inputSimulator.Keyboard.KeyPress(mediaButtonClicked);
                break;
            default:
                break;
        }
    }

    private void HandleKeyboardJsonEvent(JsonNode jsonNode)
    {
        string eventName = jsonNode["EventName"]?.ToString() ?? string.Empty;
        if (eventName != EventNames.ButtonClicked) return;

        var keyPresed = jsonNode["EventData"].Deserialize<string>();
        _inputSimulator.Keyboard.TextEntry(keyPresed);

    }
}

