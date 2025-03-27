using Qontrolr.Client.Views.Common.Controls.Base;

namespace Qontrolr.Client.Views.SubViews.KeyBoard.Controls;

internal class WindowsKeyButtons : CustomGridControl
{
    public WindowsKeyButtons(Action<Button, EventArgs> buttonsClicked)
    {
        InitializeGrid();
       
        // Create buttons dynamically using the WinButtonId enum
        foreach (WinButtonId buttonId in Enum.GetValues(typeof(WinButtonId)))
        {
            var button = CreateButton(
                buttonId.ToString().ToLower(), 
                buttonId.ToString(),          
                clicked: buttonsClicked
            );

            // Add buttons to the grid dynamically
            int index = (int)buttonId;
            AddToGrid(button, column: index % 3, row: index / 3);
        }
    }

    protected override void InitializeGrid()
    {
        Padding = 10;
        RowSpacing = 20;
        ColumnSpacing = 20;

        Padding = new Thickness(30, 15);

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
            FontSize = 18,
            CornerRadius = 20,
            WidthRequest = 100,
            HeightRequest = 100
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
