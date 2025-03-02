using System.Diagnostics;
using System.Net;
using System.Net.WebSockets;
using System.Text;

namespace Qontrolr.Server.Services;

internal class ServerSocketService
{
    //Consruct
    private bool _isListening;
    private readonly HttpListener _httpListener;

    public ServerSocketService()
    {
        _httpListener = new HttpListener();
        _httpListener.Prefixes.Add("http://localhost:5000/ws/");
    }

    public void Start() => _httpListener.Start();
    public void Stop()
    {
        _isListening = false;
        _httpListener.Stop();
    }
    
    public async Task ListenAsync()
    {
        _isListening = true;
        while (_isListening)
        {
            HttpListenerContext httpContext = null;
            try
            {
                httpContext = await _httpListener.GetContextAsync();
                if (httpContext.Request.IsWebSocketRequest)
                {
                    await HandleWebSocketConnection(httpContext);
                }
                else
                {
                    httpContext.Response.StatusCode = 400;
                    httpContext.Response.Close();
                    Debug.WriteLine("Invalid HTTP request received.");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error getting HTTP context: " + e.Message);
                break;
            }   
        }
    }

    //Helper
    private async Task HandleWebSocketConnection(HttpListenerContext httpContext)
    {
        WebSocketContext webSocketContext = null;

        try
        {
            webSocketContext = await httpContext.AcceptWebSocketAsync(subProtocol: null);
            Debug.WriteLine("WebSocket connection established.");
        }
        catch (Exception e)
        {
            httpContext.Response.StatusCode = 500;
            httpContext.Response.Close();
            Debug.WriteLine("Error accepting WebSocket connection: " + e.Message);
            return;
        }

        WebSocket webSocket = webSocketContext.WebSocket;

        byte[] buffer = new byte[1024];
        while (webSocket.State == WebSocketState.Open)
        {
            WebSocketReceiveResult result = null;

            try
            {
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error receiving WebSocket message: " + e.Message);
                break;
            }

            if (result.MessageType == WebSocketMessageType.Close)
            {
                Debug.WriteLine("WebSocket connection closed by client.");
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
            }
            else
            {
                string receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
                Debug.WriteLine("Received: " + receivedMessage);

                // Echo the message back to the client
                byte[] responseMessage = Encoding.UTF8.GetBytes("Echo: " + receivedMessage);
                await webSocket.SendAsync(new ArraySegment<byte>(responseMessage), WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

        webSocket.Dispose();
        Console.WriteLine("WebSocket connection closed.");
    }
}
