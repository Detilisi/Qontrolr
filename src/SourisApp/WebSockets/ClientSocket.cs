using WebSocketSharp;

namespace SourisApp.WebSockets;

internal class ClientSocket
{
    //Constants
    private const int ServerPort = 7890;
    private const string ServerIP = "10.0.2.2";
    private const string ServiceId = "mouse-automation";


    //Fields
    private readonly WebSocket _webSocket;

    //Construction
    public ClientSocket()
    {
        var serverRoute = $"ws://{ServerIP}:{ServerPort}/{ServiceId}";
        _webSocket = new WebSocket(serverRoute);
        
        InitializeClient();
    }

    //Initialization
    public void InitializeClient()
    {
        _webSocket.OnMessage += Client_OnMessage;
    }

    //Handlers
    private void Client_OnMessage(object? sender, MessageEventArgs e)
    {
        Console.WriteLine(e.Data);
    }

    //Public methods
    public void Connect() => _webSocket.Connect();
    public void Close() => _webSocket.Close();
    public void Send(string data) => _webSocket.Send(data);
}
