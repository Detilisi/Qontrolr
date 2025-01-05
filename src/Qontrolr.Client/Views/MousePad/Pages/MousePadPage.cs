namespace Qontrolr.Client.Views.MousePad.Pages;
using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific;
using Qontrolr.Client.Views.Common.Fonts;

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
            RowDefinitions = 
            [
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(8, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
            ],
            Children =
            {
                new Grid(){ BackgroundColor = Colors.Red }.Row(0),
                new Grid(){ BackgroundColor = Colors.Blue }.Row(1),
                ControlButton.Row(2),
            }
        };
	}

    //Controls
    private Grid ControlButton
    {
        get {
            return new Grid()
            {
                Padding = 0,
                ColumnSpacing = 5,
                ColumnDefinitions =
                [
                    new ColumnDefinition { Width = new GridLength(4, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(4, GridUnitType.Star) },
                ],
                Children =
                {
                    new Button(){ BackgroundColor = Colors.Gray, CornerRadius = 0 }.Column(0),
                    new Button(){ BackgroundColor = Colors.Gray, CornerRadius = 0 }.Column(1),
                    new Button() { BackgroundColor = Colors.Gray, CornerRadius = 0 }.Column(2),
                }
            };
        }
    }
}