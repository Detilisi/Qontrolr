using Qontrolr.Server.Services.SocketBehaviors;
using Qontrolr.SharedLib;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using WebSocketSharp.Server;

namespace Qontrolr.Server.Services;

public class ServerSocketService
{
    // Construct
    private readonly WebSocketServer _webSocketServer;
    public ServerSocketService()
    {
        HostAddress = GetHostAddress();
        _webSocketServer = new WebSocketServer(HostAddress, QontrolrConfigs.SocketPort);
        _webSocketServer.AddWebSocketService<WinAutoSocketBehavior>($"/{QontrolrConfigs.SocketEndPoint}");
    }

    public IPAddress HostAddress { get; set; }

    // Public Methods
    public void Start()
    {
        if (_webSocketServer.IsListening) return;
        _webSocketServer.Start();
    }

    public void Stop()
    {
        if (!_webSocketServer.IsListening) return;
        _webSocketServer.Stop();
    }

    //Helpers
    private static IPAddress GetHostAddress()
    {
        try
        {
            var addressList = Dns.GetHostEntry(Dns.GetHostName())?.AddressList;
            if (addressList == null || addressList.Length == 0) return IPAddress.Any;

            var ipv4Address = addressList?.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
            return ipv4Address ?? IPAddress.Any;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error initializing BaseUrl: {ex.Message}");
            return IPAddress.Any;
        }
    }
}