using Qontrolr.Shared.Common.Events;
using Qontrolr.Shared.Mouse.Button.Constants;
using Qontrolr.Shared.Mouse.Button.Enums;
using Qontrolr.Shared.Mouse.Cursor.Constants;
using Qontrolr.Shared.Mouse.Cursor.ValueObjects;
using Qontrolr.Shared.Mouse.Wheel.Constants;
using Qontrolr.Shared.Mouse.Wheel.Enums;
using QontrolrApp.Controls.Mouse;
using QontrolrApp.WebSockets;
using System.Linq.Expressions;

namespace QontrolrApp.Pages.Mouse;

public partial class TouchPadPage : ContentPage, IQueryAttributable
{
    //Fields
    private ClientSocket _webSocket;
    public string ServerUrl { get; set; }

    //Construcion
    public TouchPadPage()
	{
		InitializeComponent();

        // Add touch interaction event
        TouchPadView.Drawable = new TouchPadDrawable();
        TouchPadView.DragInteraction += MousePad_DragInteraction;
    }

    //Properties
    private int MovementScalingFactor { get; set; } = 5;
    private Point LastTouchPoint { get; set; } = new(0, 0);

    public Type ElementType => throw new NotImplementedException();

    public Expression Expression => throw new NotImplementedException();

    public IQueryProvider Provider => throw new NotImplementedException();

    //Initialize
    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        ServerUrl = query[nameof(ServerUrl)]?.ToString();
        if(string.IsNullOrEmpty(ServerUrl))
        {
            await Shell.Current.GoToAsync(".."); //Navigate back
            return;
        }
        _webSocket = new ClientSocket(ServerUrl);
        _webSocket.Connect();
    }

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