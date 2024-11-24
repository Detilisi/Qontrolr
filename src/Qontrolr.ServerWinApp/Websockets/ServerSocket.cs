using Qontrolr.Server.Websockets.WebSocketBehaviors;
using WebSocketSharp.Server;

namespace Qontrolr.Server.Websockets;

class ServerSocket
{
    //Constants
    private const int PortNumer = 7890;
   
    //Fields
    private readonly WebSocketServer _webSocketServer;

    //Construction
    public ServerSocket()
    {
        _webSocketServer = new WebSocketServer(PortNumer);
        
        InitializeServices();
    }

    //Initialization
    public void InitializeServices()
    {
        _webSocketServer.AddWebSocketService<MouseAutomation>(MouseAutomation.Endpoint);
    }

    //Public methods
    public void Start() => _webSocketServer.Start();
    public void Stop() => _webSocketServer.Stop();
}
