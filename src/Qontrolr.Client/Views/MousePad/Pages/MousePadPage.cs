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
                new TouchPad().Row(1),
                new MouseButtons().Row(2),
            }
        };
	}
}