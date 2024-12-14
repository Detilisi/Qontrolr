using Qontrolr.Shared.Common.Events;
using System.Text.Json;
using WebSocketSharp;

namespace QontrolrApp.WebSockets;

internal class ClientSocket
{
    //Constants
    private const string ServiceId = "mouse-automation";
    
    //Fields
    private readonly WebSocket _webSocket;

    //Properties
    public EventHandler<EventArgs> ClientOnError { set; private get; }

    //Construction
    public ClientSocket(string serverUrl)
    {
        var serverRoute = $"{serverUrl}/{ServiceId}";
        _webSocket = new WebSocket(serverRoute);
        _webSocket.OnError += _webSocket_OnError;
    }

    
    //Public methods
    public void Connect() => _webSocket.Connect();
    public void Close() => _webSocket.Close();
    public void Send(string data) => _webSocket.Send(data);
    public void SendEvent<T>(DeviceEvent<T> deviceEvent)
    {
        string jsonCommand = JsonSerializer.Serialize(deviceEvent);
        _webSocket.Send(jsonCommand);
    }

    //Events
    private void _webSocket_OnError(object? sender, WebSocketSharp.ErrorEventArgs e)
    {
        ClientOnError.Invoke(sender, e);
    }
}
