using Qontrolr.Client.Views.SubViews.KeyBoard.Controls;

namespace Qontrolr.Client.Views.SubViews.KeyBoard;

public class KeyPadView : ContentView
{
    //Fields
    private readonly KeyBoardViewModel _viewModel;

    //Construction
    public KeyPadView(KeyBoardViewModel viewModel)
    {
        _viewModel = viewModel;

        Content = new Grid()
        {
            RowSpacing = 1,
            RowDefinitions =
            [
                new RowDefinition { Height = new GridLength(6, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(4, GridUnitType.Star) },
            ],
            Children =
            {
                new WindowsKeyButtons(WinButton_Clicked).Row(0),
                new KeyBoardTriggerControl(Keyboard_Clicked).Row(1),
            }
        };
    }

    private void WinButton_Clicked(Button sender, EventArgs e)
    {
        var buttonId = sender.ClassId;

        // Find the matching WinButtonId based on the buttonId
        var winButtonIds = Enum.GetValues(typeof(WinButtonId)).Cast<WinButtonId>();
        var winButtonId = winButtonIds.FirstOrDefault(wb => wb.ToString().Equals(buttonId, StringComparison.OrdinalIgnoreCase));

        if (winButtonId == default) return;
        _viewModel.HandleClickedWinButtonCommand.Execute(winButtonId);
    }

    private void Keyboard_Clicked(TextChangedEventArgs e)
    {
        _viewModel.HandleClickedKeyCommand.Execute(e.NewTextValue);
    }
}
