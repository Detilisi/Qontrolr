using Qontrolr.Client.Views.Common.Controls;
using Qontrolr.Client.Views.Common.Controls.Base;
using Qontrolr.Client.Views.Common.Fonts;

namespace Qontrolr.Client.Views.SubViews.MediaPad.Controls;

internal class MediaControlButtons : CustomGridControl
{
    public MediaControlButtons(Action<MaterialIconButton, EventArgs> buttonsClicked)
    {
        InitializeGrid();

        // Create and add buttons
        var pauseButton = CreateIconButton("pause", MaterialIconsRound.Pause, Colors.White, clicked: buttonsClicked);
        var nextButton = CreateIconButton("next", MaterialIconsRound.Skip_next, Colors.White, clicked: buttonsClicked);
        var prevButton = CreateIconButton("prev", MaterialIconsRound.Skip_previous, Colors.White, clicked: buttonsClicked);

        var muteButton = CreateIconButton("mute", MaterialIconsRound.Volume_mute, Colors.White, clicked: buttonsClicked);
        var volumeUpButton = CreateIconButton("vol_up", MaterialIconsRound.Volume_up, Colors.White, clicked: buttonsClicked);
        var volumeDownButton = CreateIconButton("vol_down", MaterialIconsRound.Volume_down, Colors.White, clicked: buttonsClicked);

        // Add buttons to Grid
        AddToGrid(prevButton, column: 0, row: 1);
        AddToGrid(pauseButton, column: 1, row: 1);
        AddToGrid(nextButton, column: 2, row: 1);

        AddToGrid(volumeUpButton, column: 1, row: 0);
        AddToGrid(volumeDownButton, column: 1, row: 2);
    }

    protected override void InitializeGrid()
    {
        RowSpacing = 2;
        ColumnSpacing = 2;

        // Define a 3x3 grid
        for (int i = 0; i < 3; i++)
        {
            RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        }
    }
}

