using SourisApp.WebSockets;
using System.Diagnostics;

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

    private void OnLeftClick(object sender, EventArgs e)
    {
        var data = "{Event: CLICK,  Value: LEFT}";
        _webSocket.Send(data);
        Debug.WriteLine("Left Click triggered");
    }

    private void OnRightClick(object sender, EventArgs e)
    {
        // Placeholder for right-click functionality
        var data = "{Event: CLICK,  Value: RIGHT}";
        _webSocket.Send(data);
        Debug.WriteLine("Right Click triggered");
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

            var message = $"{deltaX},{deltaY}";
            _webSocket.Send(message);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error sending touch data: {ex.Message}");
        }
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