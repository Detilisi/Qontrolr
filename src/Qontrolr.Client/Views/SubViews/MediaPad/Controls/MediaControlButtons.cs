using CommunityToolkit.Maui.Markup;
using Qontrolr.Client.Views.Common.Controls;
using Qontrolr.Client.Views.Common.Fonts;

namespace Qontrolr.Client.Views.SubViews.MediaPad.Controls;

internal class MediaControlButtons : Grid
{
    public MediaControlButtons(Action<MaterialIconButton, EventArgs> buttonsClicked)
    {
        // Initialize Grid properties
        RowSpacing = 2;
        ColumnSpacing = 2;
        InitializeGridDefinitions();

        // Create and add buttons
        var pauseButton = CreateButton("pause", MaterialIconsRound.Pause, buttonsClicked);
        var nextButton = CreateButton("next", MaterialIconsRound.Skip_next, buttonsClicked);
        var prevButton = CreateButton("prev", MaterialIconsRound.Skip_previous, buttonsClicked);

        var muteButton = CreateButton("mute", MaterialIconsRound.Volume_mute, buttonsClicked);
        var volumeUpButton = CreateButton("vol_up", MaterialIconsRound.Volume_up, buttonsClicked);
        var volumeDownButton = CreateButton("vol_down", MaterialIconsRound.Volume_down, buttonsClicked);

        // Add buttons to Grid
        AddButtonToGrid(prevButton, column: 0, row: 1);
        AddButtonToGrid(pauseButton, column: 1, row: 1);
        AddButtonToGrid(nextButton, column: 2, row: 1);

        AddButtonToGrid(volumeUpButton, column: 1, row: 0);
        AddButtonToGrid(volumeDownButton, column: 1, row: 2);
    }

    private void InitializeGridDefinitions()
    {
        // Define a 3x3 grid
        for (int i = 0; i < 3; i++)
        {
            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
        }
    }

    private void AddButtonToGrid(MaterialIconButton button, int column, int row)
    {
        button.Column(column).Row(row);
        Children.Add(button);
    }

    private static MaterialIconButton CreateButton(
        string buttonId,
        string buttonIcon,
        Action<MaterialIconButton, EventArgs> buttonClicked)
    {
        var button = new MaterialIconButton(buttonIcon, Colors.White)
        {
            ClassId = buttonId
        };

        button.Clicked += (s, e) => buttonClicked(button, e);
        return button;
    }
}

