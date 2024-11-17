using Souris.Server.Websockets.Services;
using WebSocketSharp.Server;

namespace Souris.Server.Websockets;

class ServerWebSocket
{
    private const int PortNumer = 7890;
    private const string IP_Address = "127.0.0.1";

    private readonly WebSocketServer _webSocketServer;

    public ServerWebSocket()
    {
        var serverRoute = $"ws://{IP_Address}:{PortNumer}";
        _webSocketServer = new WebSocketServer(serverRoute);
    }

    public void Initialize()
    {
        var mouseAutomationEndpoint = "/mouse-automation";
        _webSocketServer.AddWebSocketService<MouseAutomationWebSockertBehavior>(mouseAutomationEndpoint);
        _webSocketServer.Start();
    }
}
