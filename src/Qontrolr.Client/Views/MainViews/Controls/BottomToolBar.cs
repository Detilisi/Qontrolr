using Qontrolr.Client.Views.Common.Controls;
using Qontrolr.Client.Views.Common.Controls.Base;

namespace Qontrolr.Client.Views.MainViews.Controls;

internal class BottomToolBar : CustomGridControl
{
    public BottomToolBar(Action<MaterialIconButton, EventArgs> buttonsClicked)
    {
        InitializeGrid();

        // Create and add buttons
        var mouseButton = CreateIconButton("mouse", MaterialIconsRound.Mouse, clicked: buttonsClicked);
        var keyboardButton = CreateIconButton("keyboard", MaterialIconsRound.Keyboard_alt, clicked: buttonsClicked);
        var mediapadButton = CreateIconButton("media", MaterialIconsRound.Play_circle, clicked: buttonsClicked);

        // Add buttons to Grid
        AddToGrid(mouseButton, column: 0);
        AddToGrid(keyboardButton, column: 1);
        AddToGrid(mediapadButton, column: 2);

    }

    protected override void InitializeGrid()
    {
        Padding = 10;
        ColumnSpacing = 10;

        ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
    }
}
