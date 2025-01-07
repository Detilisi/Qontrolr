namespace Qontrolr.Client.Views.MousePad.Pages;
using CommunityToolkit.Maui.Markup;
using Qontrolr.Client.Views.Common.Fonts;
using Qontrolr.Client.Views.MousePad.Controls;

public class MousePadPage : ContentPage
{
    private enum Row { ToolBar = 0, TouchPad = 1, ControlButtons = 2 }

    public MousePadPage()
	{
        Title = "Mouse Pad";
        IconImageSource = new FontImageSource()
        {
            FontFamily = MaterialIconsRound.FontFamily,
            Glyph = MaterialIconsRound.Mouse,
            Color = Colors.Black
        };

        Content = new Grid
		{
            RowSpacing = 1,
            RowDefinitions = 
            [
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(8, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
            ],
            Children =
            {
                new Grid(){ BackgroundColor = Colors.Red }.Row(0),
                new TouchPad(TrackPadPanUpdated, MouseWheelPanUpdated).Row(1),
                new MouseButtons(MouseButton_Clicked, MouseButton_Pressed, MouseButton_Released).Row(2),
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