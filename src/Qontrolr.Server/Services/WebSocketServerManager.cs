using Qontrolr.Server.Services.SocketBehaviors;
using Qontrolr.SharedLib;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using WebSocketSharp.Server;

namespace Qontrolr.Server.Services;

public class WebSocketServerManager
{
    // Construct
    private readonly WebSocketServer _webSocketServer;
    public WebSocketServerManager()
    {
        SeverUrl = GenerateQontrolrUrl(7890);
        if (string.IsNullOrWhiteSpace(SeverUrl))
        {
            throw new InvalidOperationException("Server URL is not initialized.");
        }

        _webSocketServer = new WebSocketServer(SeverUrl);
        _webSocketServer.AddWebSocketService<WinAutoSocketBehavior>($"/{QontrolrConfigs.SocketEndPoint}");
    }

    public string SeverUrl { get; set; }

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
    private static string GenerateQontrolrUrl(int port)
    {
        try
        {
            var addressList = Dns.GetHostEntry(Dns.GetHostName())?.AddressList;
            if (addressList == null || addressList.Length == 0) return string.Empty;

            var ipv4Address = addressList?.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);

            return ipv4Address != null ? $"ws://{ipv4Address}:{port}" : string.Empty;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error initializing BaseUrl: {ex.Message}");
            return string.Empty;
        }
    }
}