using Qontrolr.Client.Views.Common.Controls;

namespace Qontrolr.Client.Views.SubViews.Touchpad.Controls;

internal class TrackPad : Grid
{
    public TrackPad
    (
        Action<Frame, PanUpdatedEventArgs> trackPadPanUpdated,
        Action<Frame, PanUpdatedEventArgs> mouseWheelPanUpdated
    )
    {
        // Initialize Grid properties
        InitializeGrid();

        var trackPad = CreateFrame(
            trackPadPanUpdated,
            content: null
        );

        var scrollWheel = CreateFrame(
            mouseWheelPanUpdated,
            content: CreateMouseWheelGrid()
        );

        // Add buttons to Grid
        AddFrameToGrid(trackPad, column: 0);
        AddFrameToGrid(scrollWheel, column: 1);
    }

    private void InitializeGrid()
    {
        Padding = 0;
        ColumnSpacing = 1;

        ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(9, GridUnitType.Star) });
        ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
    }

    private void AddFrameToGrid(Frame frame, int column)
    {
        frame.Column(column);
        Children.Add(frame);
    }

    private static Frame CreateFrame(
        Action<Frame, PanUpdatedEventArgs> panUpdated,
        View? content)
    {
        var frame = new Frame
        {
            Padding = 0,
            CornerRadius = 0,
            BackgroundColor = Colors.Black,
            BorderColor = Colors.Transparent,
            Content = content
        };

        var panGesture = new PanGestureRecognizer();
        panGesture.PanUpdated += (sender, e) => panUpdated(frame, e);
        frame.GestureRecognizers.Add(panGesture);

        return frame;
    }

    private static Grid CreateMouseWheelGrid()
    {
        var grid = new Grid
        {
            RowDefinitions =
            {
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(8, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
            }
        };

        grid.Children.Add(new MaterialIconLabel(MaterialIconsRound.Keyboard_arrow_up) { TextColor = Colors.White }.Row(0));
        grid.Children.Add(new MaterialIconLabel(MaterialIconsRound.Keyboard_arrow_down) { TextColor = Colors.White }.Row(2));

        return grid;
    }
}
