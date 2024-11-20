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
    }


    private void OnConnectButtonClicked(object sender, EventArgs e)
    {
        try
        {
            IsBusy = true;
            // Connect to WebSocket server
            _webSocket.Connect();
            _webSocket.Send("Connected");

            // Update UI on successful connection
            IsBusy = false;
            ButtonPanel.IsVisible = false;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Connection Error: {ex.Message}");
            ConnectionStatusLabel.Text = "Connection Failed";
        }
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
        canvas.FillColor = Colors.LightGray;
        canvas.FillRectangle(0, 0, dirtyRect.Width, dirtyRect.Height);

        // Draw a border for the mouse pad
        canvas.StrokeColor = Colors.Black;
        canvas.StrokeSize = 2;
        canvas.DrawRectangle(0, 0, dirtyRect.Width, dirtyRect.Height);

        // Draw a centered label
        canvas.FontSize = 16;
        canvas.FontColor = Colors.DarkGray;
        canvas.DrawString("Mouse Pad", dirtyRect.Width / 2, dirtyRect.Height / 2, HorizontalAlignment.Center);
    }
}