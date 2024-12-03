using Qontrolr.Server.Websockets.WebSocketBehaviors;
using System.Net;
using WebSocketSharp.Server;

namespace Qontrolr.Server.Websockets;

public class ServerSocket
{
    //Fields
    private readonly WebSocketServer _webSocketServer;

    //Construction
    public ServerSocket()
    {
        _webSocketServer = new WebSocketServer(BaseUrl);
        
        InitializeServices();
    }

    //Properties
    public static string BaseUrl
    {
        get
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            var ipv4Address = host.AddressList.FirstOrDefault(
                ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork && ip.ToString().StartsWith("192"));

            if (ipv4Address == null) return string.Empty;

            const int  portNumber = 7890;
            return string.Format("ws://{0}:{1}", ipv4Address.ToString(), portNumber);
        }
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
