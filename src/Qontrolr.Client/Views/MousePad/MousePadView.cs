using CommunityToolkit.Maui.Markup;
using Qontrolr.Client.Views.MousePad.Controls;

namespace Qontrolr.Client.Views.MousePad;

internal class MousePadView : ContentView
{
    public MousePadView()
    {
        Content = new Grid()
        {
            RowSpacing = 1,
            RowDefinitions =
            [
                new RowDefinition { Height = new GridLength(9, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
            ],
            Children =
            {
                new TouchPad(TrackPadPanUpdated, MouseWheelPanUpdated).Row(0),
                new MouseButtons(MouseButton_Clicked, MouseButton_Pressed, MouseButton_Released).Row(1)
            }
        };
    }

    //MouseButton event Hanlders
    private void MouseButton_Clicked(Button sender, EventArgs e)
    {

    }

    private void MouseButton_Pressed(Button sender, EventArgs e)
    {

    }

    private void MouseButton_Released(Button sender, EventArgs e)
    {

    }

    //MouseWheel event hanlders
    private void MouseWheelPanUpdated(Frame sender, PanUpdatedEventArgs e)
    {

    }

    //MouseWheel event hanlders
    private void TrackPadPanUpdated(Frame sender, PanUpdatedEventArgs e)
    {

    }
}
