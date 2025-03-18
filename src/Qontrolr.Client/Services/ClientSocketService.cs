using System.Net.WebSockets;
using System.Text;

namespace Qontrolr.Client.Services;

public partial class ClientSocketService : IDisposable
{
    //Fields
    private ClientWebSocket _webSocket = new();
    
    //Locks
    private readonly object _lock = new();
    private readonly SemaphoreSlim _sendLock = new(1, 1);
    private CancellationTokenSource? _reconnectCancellation;

    //Timers and flags
    private Timer? _heartbeatTimer;
    private bool _isReconnecting;
    private int _reconnectAttempts = 0;
    
    // Constants 
    private const int ReconnectDelay = 3000; // 3 seconds between reconnect attempts
    private const int HeartbeatInterval = 30000; // 30 seconds between heartbeats
    private const int MaxReconnectAttempts = 5;

    //Properties
    public Uri? ServerUri { get; private set; }
    public bool IsConnected => _webSocket.State == WebSocketState.Open;
    public ConnectionState ConnectionState { get; private set; } = ConnectionState.Disconnected;

    //Events
    public event Action? OnConnected;
    public event Action? OnDisconnected;
    public event Action<Exception>? OnSendError;
    public event Action<Exception>? OnConnectedError;
    public event Action<ConnectionState>? OnConnectionStateChanged;
    public event Action<int, int>? OnReconnectAttempt; // Current attempt, max attempts

    //Methods
    public async Task ConnectAsync(string serverAddress, CancellationToken token = default)
    {
        // Cancel any ongoing reconnection attempt
        _reconnectCancellation?.Cancel();

        // Update connection state
        UpdateConnectionState(ConnectionState.Connecting);

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

            // Reset reconnect attempts on successful connection
            _reconnectAttempts = 0;

            // Start heartbeat
            StartHeartbeat();

            // Start receiving messages
            StartReceiving();

            UpdateConnectionState(ConnectionState.Connected);
            OnConnected?.Invoke();
        }
        catch (Exception ex)
        {
            UpdateConnectionState(ConnectionState.Failed);
            OnConnectedError?.Invoke(ex);
        }
    }

    private void StartHeartbeat()
    {
        _heartbeatTimer?.Dispose();
        _heartbeatTimer = new Timer(async _ =>
        {
            try
            {
                if (_webSocket.State == WebSocketState.Open)
                {
                    await SendAsync("{\"type\":\"heartbeat\"}", CancellationToken.None);
                }
            }
            catch
            {
                // Heartbeat failed - connection might be dead
                if (!_isReconnecting)
                {
                    TryReconnect();
                }
            }
        }, null, HeartbeatInterval, HeartbeatInterval);
    }

    private void StartReceiving()
    {
        Task.Run(async () =>
        {
            var buffer = new byte[4096];
            var receiveBuffer = new ArraySegment<byte>(buffer);

            try
            {
                while (_webSocket.State == WebSocketState.Open)
                {
                    // Receive web socket messages - add message handling if needed
                    var result = await _webSocket.ReceiveAsync(receiveBuffer, CancellationToken.None);
                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        UpdateConnectionState(ConnectionState.Disconnected);
                        OnDisconnected?.Invoke();
                        TryReconnect();
                        break;
                    }
                }
            }
            catch
            {
                // Connection closed unexpectedly
                if (_webSocket.State != WebSocketState.Closed && _webSocket.State != WebSocketState.Aborted)
                {
                    UpdateConnectionState(ConnectionState.Disconnected);
                    OnDisconnected?.Invoke();
                    TryReconnect();
                }
            }
        });
    }

    private void TryReconnect()
    {
        if (_isReconnecting || ServerUri == null) return;

        _isReconnecting = true;
        _reconnectCancellation?.Dispose();
        _reconnectCancellation = new CancellationTokenSource();

        Task.Run(async () =>
        {
            UpdateConnectionState(ConnectionState.Reconnecting);

            while (_reconnectAttempts < MaxReconnectAttempts && !_reconnectCancellation.Token.IsCancellationRequested)
            {
                _reconnectAttempts++;
                OnReconnectAttempt?.Invoke(_reconnectAttempts, MaxReconnectAttempts);

                try
                {
                    // Wait before attempting to reconnect
                    await Task.Delay(ReconnectDelay, _reconnectCancellation.Token);

                    // Try reconnecting
                    await ConnectAsync(ServerUri.Host, _reconnectCancellation.Token);

                    if (IsConnected)
                    {
                        _isReconnecting = false;
                        return;
                    }
                }
                catch
                {
                    // Failed to reconnect, try again if attempts remain
                }
            }

            // If we get here, all reconnect attempts failed
            _isReconnecting = false;
            UpdateConnectionState(ConnectionState.Failed);
        });
    }

    private void UpdateConnectionState(ConnectionState newState)
    {
        if (ConnectionState != newState)
        {
            ConnectionState = newState;
            OnConnectionStateChanged?.Invoke(newState);
        }
    }

    public async Task CloseAsync(CancellationToken token = default)
    {
        // Cancel any reconnection attempts
        _reconnectCancellation?.Cancel();
        _isReconnecting = false;

        if (_webSocket.State == WebSocketState.Open)
        {
            try
            {
                await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by user", token);
            }
            catch
            {
                // Already closed or couldn't close gracefully, force close
                _webSocket?.Dispose();
                _webSocket = new ClientWebSocket();
            }
            finally
            {
                UpdateConnectionState(ConnectionState.Disconnected);
                OnDisconnected?.Invoke();
            }
        }
    }

    public async Task SendAsync(string data, CancellationToken token = default)
    {
        if (_webSocket.State != WebSocketState.Open)
        {
            OnSendError?.Invoke(new InvalidOperationException("Socket is not connected"));
            return;
        }

        await _sendLock.WaitAsync(token);
        try
        {
            var buffer = Encoding.UTF8.GetBytes(data);
            await _webSocket.SendAsync(buffer, WebSocketMessageType.Text, true, token);
        }
        catch (Exception ex)
        {
            OnSendError?.Invoke(ex);

            // If sending failed, connection might be broken
            if (_webSocket.State != WebSocketState.Open && !_isReconnecting)
            {
                TryReconnect();
            }
        }
        finally
        {
            _sendLock.Release();
        }
    }

    public void Dispose()
    {
        _reconnectCancellation?.Cancel();
        _reconnectCancellation?.Dispose();
        _heartbeatTimer?.Dispose();
        _webSocket.Dispose();
        _sendLock.Dispose();
    }
}

// 2. Add a connection state enum
public enum ConnectionState
{
    Disconnected,
    Connecting,
    Connected,
    Reconnecting,
    Failed
}