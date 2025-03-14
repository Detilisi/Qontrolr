using System.Net.WebSockets;
using System.Text;

namespace Qontrolr.Client.Services;

public class ClientSocketService
{
    //Fields
    private ClientWebSocket _webSocket;

    //Construct
    public ClientSocketService()
    {
        _webSocket = new ClientWebSocket();
    }
    
    //Properties
    public Uri? ServerUri { get; private set; }

    //Error handler
    public event Action? OnConnected;
    public event Action? OnDisconnected;

    public event Action<Exception>? OnSendError;
    public event Action<Exception>? OnConnectedError;
    public event Action<Exception>? OnDisconnectedError;

    //Public methods
    private readonly object _lock = new();
    public async Task ConnectAsync(string serverUrl, CancellationToken token = default)
    {
        lock (_lock)
        {
            ServerUri = new Uri($"{serverUrl}/{QontrolrConfigs.SocketEndPoint}/");
            _webSocket = new ClientWebSocket();
        }

        try
        {
            await _webSocket.ConnectAsync(ServerUri, token);
            OnConnected?.Invoke();
        }
        catch(Exception ex) 
        {
            OnConnectedError?.Invoke(ex);
        }
    }

    public async Task CloseAsync(CancellationToken token = default)
    {
        await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by user", token);
        OnDisconnected?.Invoke();
    }

    //Transmit data
    private readonly SemaphoreSlim _sendLock = new(1, 1);
    public async Task SendAsync(string data, CancellationToken token = default)
    {
        await _sendLock.WaitAsync(); // Ensure only one send operation at a time
        try
        {
            var buffer = Encoding.UTF8.GetBytes(data);
            await _webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, token);
        }
        catch (Exception ex)
        {
            OnSendError?.Invoke(ex);
        }
        finally
        {
            _sendLock.Release();
        }
    }
}
