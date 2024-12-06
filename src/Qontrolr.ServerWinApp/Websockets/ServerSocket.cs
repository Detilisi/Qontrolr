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
        InitializeServices();
    }

    // Properties
    public static string BaseUrl => _baseUrl;

    // Initialization
    private static string InitializeBaseUrl()
    {
        try
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            var ipv4Address = host.AddressList.FirstOrDefault(ip =>
                ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork &&
                ip.ToString().StartsWith("192")); // Adjust this prefix as needed.

            if (ipv4Address == null)
            {
                return string.Empty;
            }

            const int portNumber = 7890;
            return $"ws://{ipv4Address}:{portNumber}";
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error initializing BaseUrl: {ex.Message}");
            return string.Empty;
        }
    }

    private void InitializeServices()
    {
        // Register WebSocket services
        _webSocketServer.AddWebSocketService<MouseAutomation>(MouseAutomation.Endpoint);
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