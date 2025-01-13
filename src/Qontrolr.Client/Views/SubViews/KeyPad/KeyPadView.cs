using CommunityToolkit.Maui.Markup;
using Qontrolr.Client.Views.SubViews.KeyPad.Controls;

namespace Qontrolr.Client.Views.SubViews.KeyPad;

internal class KeyPadView : ContentView
{
	public KeyPadView()
	{
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
                new KeyBoardTriggerControl(Keyboard_Clicked).Row(1),
            }
        };
    }

    //Handlers
    private void Keypad_Clicked(Button sender, EventArgs e)
    {
    }

    private void Keyboard_Clicked(TextChangedEventArgs e)
    {
    }
}
