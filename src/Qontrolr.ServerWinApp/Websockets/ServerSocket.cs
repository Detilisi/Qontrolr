using Qontrolr.Server.Websockets.WebSocketBehaviors;
using System.Diagnostics;
using System.Net;
using WebSocketSharp.Server;

namespace Qontrolr.Server.Websockets;

public class ServerSocket
{
    // Fields
    private readonly WebSocketServer _webSocketServer;
    private static readonly string _baseUrl;

    // Static Constructor for Initializing BaseUrl
    static ServerSocket()
    {
        _baseUrl = InitializeBaseUrl();
        if (string.IsNullOrWhiteSpace(_baseUrl))
        {
            Debug.WriteLine("Failed to determine server base URL.");
        }
    }

    // Constructor
    public ServerSocket()
    {
        if (string.IsNullOrWhiteSpace(_baseUrl))
        {
            throw new InvalidOperationException("Server base URL is not initialized.");
        }

        _webSocketServer = new WebSocketServer(_baseUrl);
        
        // Register WebSocket services
        _webSocketServer.AddWebSocketService<MouseAutomation>(MouseAutomation.Endpoint);
    }

    // Properties
    public static string BaseUrl => _baseUrl;

    // Initialization
    private static string InitializeBaseUrl()
    {
        try
        {
            const int PortNumber = 7890;
            const System.Net.Sockets.AddressFamily AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork;

            var addressList = Dns.GetHostEntry(Dns.GetHostName())?.AddressList;

            if (addressList == null || addressList.Length == 0) return string.Empty;

            var ipv4Address = addressList
                .FirstOrDefault(ip => ip.AddressFamily == AddressFamily && IsTwoDigitSuffix(ip.ToString()));

            return ipv4Address != null ? $"ws://{ipv4Address}:{PortNumber}" : string.Empty;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error initializing BaseUrl: {ex.Message}");
            return string.Empty;
        }
    }

    private static bool IsTwoDigitSuffix(string ipAddress)
    {
        var segments = ipAddress.Split('.');
        return segments.Length == 4
            && int.TryParse(segments[3], out int lastSegment)
            && lastSegment is >= 10 and <= 99; // Compact two-digit check
    }

    // Public Methods
    public void Start()
    {
        if (_webSocketServer.IsListening)
        {
            Debug.WriteLine("WebSocket server is already running.");
            return;
        }

        _webSocketServer.Start();
        Debug.WriteLine($"WebSocket server started at {BaseUrl}");
    }

    public void Stop()
    {
        if (!_webSocketServer.IsListening)
        {
            Debug.WriteLine("WebSocket server is not running.");
            return;
        }

        _webSocketServer.Stop();
        Debug.WriteLine("WebSocket server stopped.");
    }
}