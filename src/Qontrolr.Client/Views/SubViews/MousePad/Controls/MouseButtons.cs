using Qontrolr.Client.Views.Common.Controls.Base;

namespace Qontrolr.Client.Views.SubViews.MousePad.Controls;

internal class MouseButtons : CustomGridControl
{
    public MouseButtons
    (
        Action<Button, EventArgs> buttonsClicked,
        Action<Button, EventArgs> buttonsPressed,
        Action<Button, EventArgs> buttonsReleased
    )
    {
        InitializeGrid();

        // Create buttons
        var leftButton = CreateIconButton("L", string.Empty, clicked: buttonsClicked, pressed: buttonsPressed, released: buttonsReleased);
        var middleButton = CreateIconButton("M", string.Empty, clicked: buttonsClicked, pressed: buttonsPressed, released: buttonsReleased);
        var rightButton = CreateIconButton("R", string.Empty, clicked: buttonsClicked, pressed: buttonsPressed, released: buttonsReleased);

        leftButton.BackgroundColor = Colors.Black;
        middleButton.BackgroundColor = Colors.Black;
        rightButton.BackgroundColor = Colors.Black;

        // Add buttons to Grid
        AddToGrid(leftButton, column: 0);
        AddToGrid(middleButton, column: 1);
        AddToGrid(rightButton, column: 2);
    }

    protected override void InitializeGrid()
    {
        ColumnSpacing = 2;

        ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(4, GridUnitType.Star) });
        ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
        ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(4, GridUnitType.Star) });
    }
}

