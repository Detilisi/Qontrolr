using Souris.Server.Websockets.Services;
using WebSocketSharp.Server;

namespace Souris.Server.Websockets;

class ServerWebSocket
{
    //Constants
    private const int PortNumer = 7890;
    private const string IP_Address = "127.0.0.1";

    //Fields
    private readonly WebSocketServer _webSocketServer;

    //Construction
    public ServerWebSocket()
    {
        var serverRoute = $"ws://{IP_Address}:{PortNumer}";
        _webSocketServer = new WebSocketServer(serverRoute);

        InitializeServices();
    }

    //Initialization
    public void InitializeServices()
    {
        var mouseAutomationEndpoint = "/mouse-automation";
        _webSocketServer.AddWebSocketService<MouseAutomationWebSockertBehavior>(mouseAutomationEndpoint);
    }

    //Public methods
    public void Start() => _webSocketServer.Start();
    public void Stop() => _webSocketServer.Stop();
    
}
