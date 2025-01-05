using CommunityToolkit.Maui.Markup;
using Qontrolr.Client.Views.Common.Fonts;
using System.Diagnostics;

namespace Qontrolr.Client.Views.MousePad.Controls;

internal class TouchPad : Grid
{
    //Fields
    private Frame TrackPad = new()
    {
        Padding = 0,
        CornerRadius = 0,
        BackgroundColor = Colors.Gray,
        BorderColor = Colors.Transparent,

        Content = new Label()
        {
            FontSize = 32,
            Text = MaterialIconsRound.Mouse,
            FontFamily = MaterialIconsRound.FontFamily,

            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
        }
    };

    private Frame MouseWheel = new()
    {
        Padding = 0,
        CornerRadius = 0,
        BackgroundColor = Colors.Gray,
        BorderColor = Colors.Transparent,

        Content = new Grid()
        {
            RowDefinitions =
            [
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(8, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
            ],
            Children =
            {
                new Label()
                {
                    FontSize = 32,
                    Text = MaterialIconsRound.Keyboard_arrow_up,
                    FontFamily = MaterialIconsRound.FontFamily,

                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                }.Row(0),
                new BoxView
                {
                    Color = Colors.Black,
                    WidthRequest = 2,
                    VerticalOptions = LayoutOptions.Fill
                }.Row(1),
                new Label()
                {
                    FontSize = 32,
                    Text = MaterialIconsRound.Keyboard_arrow_down,
                    FontFamily = MaterialIconsRound.FontFamily,

                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                }.Row(2),
            }
        }
    };

    //Construction
    public TouchPad()
    {
        var trackPadPanGesture = new PanGestureRecognizer();
        trackPadPanGesture.PanUpdated += TrackPadPanGesture_PanUpdated;

        var mouseWheelPanGesture = new PanGestureRecognizer();
        mouseWheelPanGesture.PanUpdated += MouseWheelPanGesture_PanUpdated;

        TrackPad.GestureRecognizers.Add(trackPadPanGesture);
        MouseWheel.GestureRecognizers.Add(mouseWheelPanGesture);

        Padding = 0;
        ColumnSpacing = 1;
        BackgroundColor = Colors.Gray;
        ColumnDefinitions =
        [
            new ColumnDefinition { Width = new GridLength(9, GridUnitType.Star) },
            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
        ];

        Children.Add(TrackPad.Column(0));
        Children.Add(MouseWheel.Column(1));
    }

    //Event handlers
    private void MouseWheelPanGesture_PanUpdated(object? sender, PanUpdatedEventArgs e)
    {
        // Send scroll event to the WebSocket
        if ((int)e.TotalY > 0)
        {
            //scroll up
            Debug.WriteLine("scroll up");
        }
        else if ((int)e.TotalY < 0)
        {
            //scroll down
            Debug.WriteLine("scroll down");
        }
    }

    private void TrackPadPanGesture_PanUpdated(object? sender, PanUpdatedEventArgs e)
    {
        var x = e.TotalX;
        var y = e.TotalY;

        Debug.WriteLine($"X: {e.TotalX:F2}, Y: {e.TotalY:F2}");
    }
}
