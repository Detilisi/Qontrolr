namespace Qontrolr.Client.Views.MousePad.Pages;
using CommunityToolkit.Maui.Markup;
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
                TouchPadControl.Row(1),
                MouseButtonControl.Row(2),
            }
        };
	}

    //Controls
    private Grid MouseButtonControl
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

    private Grid TouchPadControl
    {
        get
        {
            return new Grid()
            {
                Padding = 0,
                ColumnSpacing = 1,
                ColumnDefinitions =
                [
                    new ColumnDefinition { Width = new GridLength(9, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                ],
                Children =
                {
                    new Frame()
                    {
                        Padding = 0,
                        CornerRadius = 0,
                        BackgroundColor = Colors.Blue, 
                        
                        Content = new Label()
                        {
                            FontSize = 32,
                            Text = MaterialIconsRound.Swipe,
                            FontFamily = MaterialIconsRound.FontFamily,
                            
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.Center,
                        }
                    }.Column(0),
                    new Frame()
                    {
                        Padding = 0,
                        CornerRadius = 0,
                        BackgroundColor = Colors.Yellow,

                        Content = new Label()
                        {
                            FontSize = 32,
                            Text = MaterialIconsRound.Height,
                            FontFamily = MaterialIconsRound.FontFamily,

                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.Center,
                        }

                    }.Column(1)
                }
            };
        }
    }
}