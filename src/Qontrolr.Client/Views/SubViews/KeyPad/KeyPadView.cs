using CommunityToolkit.Maui.Markup;
using Qontrolr.Client.Views.SubViews.KeyPad.Controls;

namespace Qontrolr.Client.Views.SubViews.KeyPad;

internal class KeyPadView : ContentView
{
	public KeyPadView()
	{
        var entryView = new Entry()
        {
            TextColor = Colors.Transparent,
        };
        //entryView.Focus();

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
                new WindowsKeyButtons(Keypad_Clicked).Row(0),
                new KeyBoardTriggerControl(null).Row(1),
            }
        };
    }

    private void Keypad_Clicked(Button sender, EventArgs e)
    {
    }
}
