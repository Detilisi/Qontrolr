using SourisApp.WebSockets;
using System.Diagnostics;

namespace SourisApp.Pages;

public partial class MousePadPage : ContentPage
{
    private ClientWebSocket _webSocket;
    private Point _lastTouchPoint = new Point(0, 0);

    private Button _connectButton;
    private Label _connectionStatusLabel;
    private GraphicsView _mousePadView;

    public MousePadPage()
    {
        // Initialize WebSocket client
        _webSocket = new ClientWebSocket();

        Initilize();
    }

    private void Initilize()
    {
        // Connection button
        _connectButton = new Button
        {
            Text = "Connect to Server",
            BackgroundColor = Colors.Green,
            TextColor = Colors.White,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };
        _connectButton.Clicked += OnConnectButtonClicked;

        // Connection status label
        _connectionStatusLabel = new Label
        {
            Text = "Not connected",
            TextColor = Colors.Red,
            FontSize = 14,
            HorizontalOptions = LayoutOptions.Center
        };

        // Add GraphicsView
        _mousePadView = new GraphicsView
        {
            Drawable = new MousePadDrawable(),
            BackgroundColor = Colors.LightGray,
            HeightRequest = 300,
            WidthRequest = 300
        };

        // Touch tracking
        _mousePadView.DragInteraction += GameView_DragInteraction;
        
        // Layout
        Content = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 20,
            Children =
            {
                _connectionStatusLabel,
                _connectButton,
                _mousePadView
            }
        };
    }

    //Event handlers
    private void OnConnectButtonClicked(object sender, EventArgs e)
    {
        _webSocket.Connect();
        _webSocket.Send("Connected");
        _connectionStatusLabel.Text = "Connected";
        _connectionStatusLabel.TextColor = Colors.Green;

        _connectButton.IsEnabled = false;
    }

    void GameView_DragInteraction(object sender, TouchEventArgs e)
    {
        var touchPoint = e.Touches.First();
        var movementScalingFactor = 5;

        // Calculate movement delta
        var deltaX = (int)(touchPoint.X - _lastTouchPoint.X) * movementScalingFactor;
        var deltaY = (int)(touchPoint.Y - _lastTouchPoint.Y) * movementScalingFactor;

        _lastTouchPoint = touchPoint;

        Debug.WriteLine(deltaX + ", " + deltaY);
        _webSocket.Send($"{deltaX}, {deltaY}");
    }
}

public class MousePadDrawable : IDrawable
{
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
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