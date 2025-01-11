using CommunityToolkit.Maui.Markup;

namespace Qontrolr.Client.Views.SubViews.MousePad.Controls;

internal class MouseButtons : Grid
{
    public MouseButtons
    (
        Action<Button, EventArgs> buttonsClicked,
        Action<Button, EventArgs> buttonsPressed,
        Action<Button, EventArgs> buttonsReleased
    )
    {
        // Initialize Grid
        InitializeGrid();

        // Create buttons
        var leftButton = CreateButton("L", buttonsClicked, buttonsPressed, buttonsReleased);
        var middleButton = CreateButton("M", buttonsClicked, buttonsPressed, buttonsReleased);
        var rightButton = CreateButton("R", buttonsClicked, buttonsPressed, buttonsReleased);

        // Add buttons to Grid
        AddButtonToGrid(leftButton, column: 0);
        AddButtonToGrid(middleButton, column: 1);
        AddButtonToGrid(rightButton, column: 2);
    }

    private void InitializeGrid()
    {
        ColumnSpacing = 2;

        ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(4, GridUnitType.Star) });
        ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
        ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(4, GridUnitType.Star) });
    }

    private void AddButtonToGrid(Button button, int column)
    {
        button.Column(column);
        Children.Add(button);
    }

    private static Button CreateButton(
        string buttonId,
        Action<Button, EventArgs> onClicked,
        Action<Button, EventArgs> onPressed,
        Action<Button, EventArgs> onReleased)
    {
        var button = new Button
        {
            CornerRadius = 0,
            ClassId = buttonId,
            BackgroundColor = Colors.Black,
        };

        button.Clicked += (s, e) => onClicked(button, e);
        button.Pressed += (s, e) => onPressed(button, e);
        button.Released += (s, e) => onReleased(button, e);

        return button;
    }
}

