using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace Qontrolr.Client.Services;

public class ClientSocketService
{
    //Fields
    private ClientWebSocket _webSocket;

    //Properties
    public Uri? ServerUri { get; private set; }
    public event EventHandler<EventArgs>? ErrorOccurred;    

    //Construction
    public ClientSocketService() => _webSocket = new ClientWebSocket();
    
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
        }
        catch(Exception ex) 
        {
            var message = ex.Message;
            ErrorOccurred?.Invoke(this, EventArgs.Empty);
        }
    }

    public async Task CloseAsync(CancellationToken token = default)
    {
        await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by user", token);
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
            var message = ex.Message;
            ErrorOccurred?.Invoke(this, EventArgs.Empty);
        }
        finally
        {
            _sendLock.Release();
        }
    }

    public async Task SendDeviceEventAsync<T>(DeviceId device, string name, T data)
    {
        var deviceEvent = new DeviceEvent<T>(device, name, data);
        string jsonCommand = JsonSerializer.Serialize(deviceEvent);
        await SendAsync(jsonCommand);
    }
}
