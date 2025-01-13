using Qontrolr.Client.Views.Common.Controls;
using Qontrolr.Client.Views.Common.Controls.Base;
using Qontrolr.Client.Views.Common.Fonts;

namespace Qontrolr.Client.Views.SubViews.KeyPad.Controls;

internal class KeyBoardTriggerControl : CustomGridControl
{
    private readonly Entry _keyboardEntry;
    private readonly MaterialIconButton _showKeyboarButton;

    public KeyBoardTriggerControl(Action<TextChangedEventArgs> textChanged)
    {
        // Create components
        _keyboardEntry = new Entry();
        _showKeyboarButton = new MaterialIconButton(MaterialIconsRound.Keyboard_hide, 60);
        
        _keyboardEntry.TextColor = Colors.Transparent;
        _keyboardEntry.TextChanged += (s, e) => textChanged(e);
        _showKeyboarButton.Clicked += ShowKeyboarButton_Clicked;

        InitializeGrid();

        // Add components
        AddToGrid(_keyboardEntry, row: 0);
        AddToGrid(_showKeyboarButton, row: 0);
    }

    protected override void InitializeGrid()
    {
        RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
    }

    private void ShowKeyboarButton_Clicked(object? sender, EventArgs e)
    {
        _keyboardEntry.Focus();
    }
}
