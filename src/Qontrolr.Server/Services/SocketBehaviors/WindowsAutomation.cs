using Qontrolr.Server.Services.Handlers;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Qontrolr.Server.Services.SocketBehaviors;

internal class WindowsAutomation : WebSocketBehavior
{
    //Fields
    private readonly TouchPadJsonNodeHandler _touchPadJsonNodeHandler;
    private readonly KeyboardJsonNodeHandler _keyboardJsonNodeHandler;
    private readonly MediaKeysJsonNodeHandler _mediaKeysJsonNodeHandler;

    //Properties
    public static string Endpoint => "/qontrolr";

    //Construct
    public WindowsAutomation()
    {
        _touchPadJsonNodeHandler = new TouchPadJsonNodeHandler();
        _keyboardJsonNodeHandler = new KeyboardJsonNodeHandler();
        _mediaKeysJsonNodeHandler = new MediaKeysJsonNodeHandler();
    }   

    //Override
    protected override void OnOpen()
    {
        Console.WriteLine($"Client connected: {ID}");
        Send($"{ID}");   
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
                    _touchPadJsonNodeHandler.Handle(jsonNode);
                    break;
                case DeviceId.Keyboard:
                    _keyboardJsonNodeHandler.Handle(jsonNode);
                    break;
                case DeviceId.MediaKeys:
                    _mediaKeysJsonNodeHandler.Handle(jsonNode);
                    break;
                default:
                    break;
            }
        }
        catch (Exception ex)
        {
            Send(ex.Message);
            Console.WriteLine(ex.Message);
        }
    }
}

