using CommunityToolkit.Maui.Markup;
using Qontrolr.Client.Views.Common.Controls;
using Qontrolr.Client.Views.Common.Fonts;

namespace Qontrolr.Client.Views.MainViews.Controls;

internal class BottomToolBar : Grid 
{
    public BottomToolBar(Action<MaterialIconButton, EventArgs> buttonsClicked)
    {
        // Initialize Grid properties
        InitializeGridDefinitions();

        // Create and add buttons
        var mouseButton = CreateButton("mouse", MaterialIconsRound.Mouse, buttonsClicked);
        var keyboardButton = CreateButton("keyboard", MaterialIconsRound.Keyboard_alt, buttonsClicked);
        var gamepadButton = CreateButton("gamepad", MaterialIconsRound.Sports_esports, buttonsClicked);
        var mediapadButton = CreateButton("media", MaterialIconsRound.Play_circle, buttonsClicked);

        // Add buttons to Grid
        AddButtonToGrid(mouseButton, column: 0);
        AddButtonToGrid(keyboardButton, column: 1);
        AddButtonToGrid(gamepadButton, column: 2);
        AddButtonToGrid(mediapadButton, column: 3);

    }

    private void InitializeGridDefinitions()
    {
        Padding = 10;
        ColumnSpacing = 10;

        ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
    }

    private void AddButtonToGrid(MaterialIconButton button, int column = 0, int row = 0)
    {
        button.Column(column).Row(row);
        Children.Add(button);
    }

    private MaterialIconButton CreateButton
    (
        string buttonId,
        string buttonIcon,
        Action<MaterialIconButton, EventArgs> buttonClicked
    )
    {
        var button = new MaterialIconButton(buttonIcon)
        {
            ClassId = buttonId
        };

        button.Clicked += (s, e) => buttonClicked(button, e);
        return button;
    }
}
