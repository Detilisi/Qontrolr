﻿using CommunityToolkit.Maui.Markup;
using Qontrolr.Client.Views.Common.Controls;
using Qontrolr.Client.Views.Common.Fonts;
using System.Diagnostics;

namespace Qontrolr.Client.Views.MousePad.Controls;

internal class TouchPad : Grid
{
    //Construction
    public TouchPad
    (
        Action<PanUpdatedEventArgs> mouseWheelHandler,
        Action<PanUpdatedEventArgs> trackPadhandler
    )
    {
        //Initialize frames
        var trackPad = CreateTrackPadFrame(trackPadhandler);
        var mouseWheel = CreateMouseWheelFrame(mouseWheelHandler);

        //Set up Grid
        Padding = 0;
        ColumnSpacing = 1;
        ColumnDefinitions =
        [
            new ColumnDefinition { Width = new GridLength(9, GridUnitType.Star) },
            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
        ];

        Children.Add(trackPad.Column(0));
        Children.Add(mouseWheel.Column(1));
    }

    //Helper method
    private Frame CreateMouseWheelFrame
    (
         Action<PanUpdatedEventArgs> handler
    )
    {
        var mouseWheelFrame = new Frame()
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
                    new MaterialIconLabel(MaterialIconsRound.Keyboard_arrow_up).Row(0),
                    new MaterialIconLabel(MaterialIconsRound.Keyboard_arrow_down).Row(2),
                    new BoxView { WidthRequest = 2, Color = Colors.Black, VerticalOptions = LayoutOptions.Fill }.Row(1),
                }
            }
        };

        var mouseWheelPanGesture = new PanGestureRecognizer();
        mouseWheelPanGesture.PanUpdated += (sender, e) => { handler.Invoke(e); };
        mouseWheelFrame.GestureRecognizers.Add(mouseWheelPanGesture);

        return mouseWheelFrame;
    }

    private Frame CreateTrackPadFrame
    (
        Action<PanUpdatedEventArgs> handler
    )
    {
        var trackPadFrame = new Frame()
        {
            Padding = 0,
            CornerRadius = 0,
            BackgroundColor = Colors.Gray,
            BorderColor = Colors.Transparent,
            Content = new MaterialIconLabel(MaterialIconsRound.Mouse)
        };

        var trackPadPanGesture = new PanGestureRecognizer();
        trackPadPanGesture.PanUpdated += (sender, e) => { handler.Invoke(e); };
        trackPadFrame.GestureRecognizers.Add(trackPadPanGesture);

        return trackPadFrame;
    }

}