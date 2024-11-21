using Souris.Shared;
using SourisApp.WebSockets;
using System.Diagnostics;
using System.Text.Json;

namespace SourisApp;

public partial class MainPage : ContentPage
{
    private readonly ClientSocket _webSocket;
    private Point _lastTouchPoint = new(0, 0);

    public MainPage()
    {
        InitializeComponent();

        _webSocket = new ClientSocket();

        // Add touch interaction event
        MousePadView.Drawable = new MousePadDrawable();
        MousePadView.DragInteraction += MousePad_DragInteraction;

        // Show the modal on startup
        ConnectionModal.IsVisible = true;
    }
 
    //Event handlers
    private async void OnConnectButtonClicked(object sender, EventArgs e)
    {
        try
        {
            // Connect to WebSocket server
            _webSocket.Connect();
            _webSocket.Send("Connected");

            // Hide the modal on successful connection
            ConnectionModal.IsVisible = false;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Connection Error: {ex.Message}");
            await DisplayAlert("Connection Failed", "Unable to connect to the server.", "OK");
        }
    }

    private void OnRightClick(object sender, EventArgs e)
    {
        SendCommand(Commands.Click, "0");
        Debug.WriteLine("Right Click triggered");
    }

    private void OnLeftClick(object sender, EventArgs e)
    {
        SendCommand(Commands.Click, "1");
        Debug.WriteLine("Left Click triggered");
    }

    private void MousePad_DragInteraction(object sender, TouchEventArgs e)
    {
        try
        {
            var touchPoint = e.Touches.First();
            var movementScalingFactor = 5;

            // Calculate movement delta
            var deltaX = (int)(touchPoint.X - _lastTouchPoint.X) * movementScalingFactor;
            var deltaY = (int)(touchPoint.Y - _lastTouchPoint.Y) * movementScalingFactor;

            _lastTouchPoint = touchPoint;

            var data = $"{deltaX},{deltaY}";
            SendCommand(Commands.MoveCursor, data);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error sending touch data: {ex.Message}");
        }
    }

    //Helper methods
    private void SendCommand(string eventId, string data)
    {
        var command = new CommandModel
        {
            Name = eventId,
            Data = data
        };

        string jsonCommand = JsonSerializer.Serialize(command);
        _webSocket.Send(jsonCommand);
    }
}

//Helper class
public class MousePadDrawable : IDrawable
{
    public static MousePadDrawable Instance { get; } = new MousePadDrawable();

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        // Add centered text
        canvas.FontSize = 18;
        canvas.FontColor = Colors.LightGray;
        canvas.DrawString("Touchpad Area", dirtyRect.Width / 2, dirtyRect.Height / 2, HorizontalAlignment.Center);
    }


}