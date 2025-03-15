using System.Net.WebSockets;
using System.Text;

namespace Qontrolr.Client.Services;

public partial class ClientSocketService : IDisposable
{
    private ClientWebSocket _webSocket = new();

    private readonly object _lock = new();
    private readonly SemaphoreSlim _sendLock = new(1, 1);

    public Uri? ServerUri { get; private set; }
    public event Action? OnConnected;
    public event Action? OnDisconnected;
    public event Action<Exception>? OnSendError;
    public event Action<Exception>? OnConnectedError;

    public async Task ConnectAsync(string serverAddress, CancellationToken token = default)
    {
        lock (_lock)
        {
            ServerUri = new UriBuilder
            {
                Scheme = "ws",
                Host = serverAddress,
                Port = QontrolrConfigs.SocketPort,
                Path = $"/{QontrolrConfigs.SocketEndPoint}"
            }.Uri;

            _webSocket?.Dispose();
            _webSocket = new ClientWebSocket();
        }

        try
        {
            await _webSocket.ConnectAsync(ServerUri, token);
            OnConnected?.Invoke();
        }
        catch (Exception ex)
        {
            OnConnectedError?.Invoke(ex);
        }
    }

    public async Task CloseAsync(CancellationToken token = default)
    {
        if (_webSocket.State == WebSocketState.Open)
        {
            await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by user", token);
            OnDisconnected?.Invoke();
        }
    }

    public async Task SendAsync(string data, CancellationToken token = default)
    {
        if (_webSocket.State != WebSocketState.Open) return;

        await _sendLock.WaitAsync(token);
        try
        {
            var buffer = Encoding.UTF8.GetBytes(data);
            await _webSocket.SendAsync(buffer, WebSocketMessageType.Text, true, token);
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

    public void Dispose()
    {
        _webSocket.Dispose();
        _sendLock.Dispose();
    }
}
