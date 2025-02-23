using System.Net.WebSockets;
using System.Text;

namespace Qontrolr.Client.Services;

public class WebSocketService
{
    //Fields
    private readonly ClientWebSocket _webSocket;

    //Properties
    public event EventHandler<EventArgs>? ErrorOccurred;
    public bool IsConnected => _webSocket.State == WebSocketState.Open;

    //Construction
    public WebSocketService()
    {
        _webSocket = new ClientWebSocket();
    }

    //Public methods
    public async Task ConnectAsync(string serverUrl, CancellationToken token = default)
    {
        try
        {
            var serverRoute = $"{serverUrl}/qontrolr";
            await _webSocket.ConnectAsync(new Uri(serverRoute), token);
        }
        catch
        {
            ErrorOccurred?.Invoke(this, EventArgs.Empty);
        }
    }

    public async Task CloseAsync(CancellationToken token = default)
    {
        await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by user", token);
    }

    //Transmit data
    public async Task SendAsync(string data, CancellationToken token = default)
    {
        try
        {
            var buffer = Encoding.UTF8.GetBytes(data);
            await _webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, token);
        }
        catch
        {
            ErrorOccurred?.Invoke(this, EventArgs.Empty);
        }
    }
}
