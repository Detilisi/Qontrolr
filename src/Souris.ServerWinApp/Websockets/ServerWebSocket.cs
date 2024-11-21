using Souris.Server.Websockets.Services;
using WebSocketSharp.Server;

namespace Souris.Server.Websockets;

class ServerWebSocket
{
    //Constants
    private const int PortNumer = 7890;
   
    //Fields
    private readonly WebSocketServer _webSocketServer;

    //Construction
    public ServerWebSocket()
    {
        _webSocketServer = new WebSocketServer(PortNumer);
        
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
