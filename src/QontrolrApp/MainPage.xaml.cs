using Qontrolr.Shared.Common.Events;
using Qontrolr.Shared.Mouse.Button.Constants;
using Qontrolr.Shared.Mouse.Button.Enums;
using Qontrolr.Shared.Mouse.Cursor.Constants;
using Qontrolr.Shared.Mouse.Cursor.ValueObjects;
using Qontrolr.Shared.Mouse.Wheel.Constants;
using Qontrolr.Shared.Mouse.Wheel.Enums;
using QontrolrApp.WebSockets;

namespace QontrolrApp;

public partial class MainPage : ContentPage
{
    //Fields
    private readonly ClientSocket _webSocket;
    
    //Construction
    public MainPage()
    {
        InitializeComponent();

        _webSocket = new ClientSocket();

        // Add touch interaction event
        MousePadView.Drawable = new MousePadDrawable();
        MousePadView.DragInteraction += MousePad_DragInteraction;

        // Show the modal on startup
        _webSocket.Connect();
    }

    //Properties
    private int MovementScalingFactor { get; set; } = 5;
    private Point LastTouchPoint { get; set; } = new(0, 0);
    
    //Event handlers
    private async void MousePad_DragInteraction(object sender, TouchEventArgs e)
    {
        try
        {
            var touchPoint = e.Touches.First();
            
            // Calculate movement delta
            var deltaX = (int)(touchPoint.X - LastTouchPoint.X) * MovementScalingFactor;
            var deltaY = (int)(touchPoint.Y - LastTouchPoint.Y) * MovementScalingFactor;

            //Send event
            var cursorMovedEvent = new DeviceEvent<CursorPosition>(
                CursorEvents.CursorMoved, new CursorPosition(deltaX, deltaY)
            );
            _webSocket.SendEvent(cursorMovedEvent);

            //Update last touch point
            LastTouchPoint = touchPoint;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Failed", $"Error sending touch data. {ex.Message}", "OK");
        }
    }

    private void OnMouseWheelScrolled(object sender, ValueChangedEventArgs e)
    {
        // Send scroll event to the WebSocket
        if ((int)e.OldValue > 0)
        {
            _webSocket.SendEvent(new DeviceEvent<ScrollDirection>(WheelEvents.WheelScrolled, ScrollDirection.Up));
        }
        else if ((int)e.OldValue < 0)
        {
            _webSocket.SendEvent(new DeviceEvent<ScrollDirection>(WheelEvents.WheelScrolled, ScrollDirection.Down));
        }
            
        // Reset slider position after the event is sent
        MouseWheelSlider.Value = 0;
    }

    private void OnRightClick(object sender, EventArgs e)
    {
        _webSocket.SendEvent(new DeviceEvent<ButtonId>(ButtonEvents.ButtonClick, ButtonId.Right));
    }

    private void OnLeftClick(object sender, EventArgs e)
    {
        _webSocket.SendEvent(new DeviceEvent<ButtonId>(ButtonEvents.ButtonClick, ButtonId.Left));
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