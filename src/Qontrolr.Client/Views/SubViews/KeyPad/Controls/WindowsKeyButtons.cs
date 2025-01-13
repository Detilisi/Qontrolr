using Qontrolr.Client.Views.Common.Controls.Base;

namespace Qontrolr.Client.Views.SubViews.KeyPad.Controls;

internal class WindowsKeyButtons : CustomGridControl
{
    public WindowsKeyButtons(Action<Button, EventArgs> buttonsClicked)
    {
        InitializeGrid();

        // Create and add buttons
        var escapeButton = CreateButton("escape", "Esc", clicked: buttonsClicked);
        var tabButton = CreateButton("tab", "Tab", clicked: buttonsClicked);
        var windowButton = CreateButton("win", "Win", clicked: buttonsClicked);

        var shiftButton = CreateButton("shft", "Shft", clicked: buttonsClicked);
        var controlButton = CreateButton("ctrl", "Ctrl", clicked: buttonsClicked);
        var altButton = CreateButton("alt", "Alt", clicked: buttonsClicked);

        var functionButton = CreateButton("fn", "Fn", clicked: buttonsClicked);
        var insertButton = CreateButton("insrt", "Insrt", clicked: buttonsClicked);
        var printButton = CreateButton("prt", "Prt", clicked: buttonsClicked);

        // Add buttons to Grid
        AddToGrid(escapeButton, column: 0, row: 0);
        AddToGrid(tabButton, column: 1, row: 0);
        AddToGrid(windowButton, column: 2, row: 0);

        AddToGrid(shiftButton, column: 0, row: 1);
        AddToGrid(controlButton, column: 1, row: 1);
        AddToGrid(altButton, column: 2, row: 1);

        AddToGrid(functionButton, column: 0, row: 2);
        AddToGrid(insertButton, column: 1, row: 2);
        AddToGrid(printButton, column: 2, row: 2);
    }

    protected override void InitializeGrid()
    {
        Padding = 10;
        RowSpacing = 5;
        ColumnSpacing = 5;

        // Define a 3x3 grid
        for (int i = 0; i < 3; i++)
        {
            RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        }
    }

    protected Button CreateButton
    (
        string buttonId,
        string buttonText,
        Action<Button, EventArgs>? clicked = null,
        Action<Button, EventArgs>? pressed = null,
        Action<Button, EventArgs>? released = null
    )
    {
        var button = new Button()
        {
            Text = buttonText,
            ClassId = buttonId,
            CornerRadius = 20,
            TextColor = Colors.White,
            BackgroundColor = Colors.Black,
        };

        if (clicked != null)
            button.Clicked += (s, e) => clicked(button, e);
        if (pressed != null)
            button.Pressed += (s, e) => pressed(button, e);
        if (released != null)
            button.Released += (s, e) => released(button, e);

        return button;
    }
}
