using CommunityToolkit.Maui.Markup;
using Qontrolr.Client.Views.Common.Controls;
using Qontrolr.Client.Views.Common.Fonts;
using Qontrolr.Client.Views.MousePad;

namespace Qontrolr.Client.Views;

public class MainPage : ContentPage
{
	public MainPage()
	{
        var currentView = new MousePadView();
        var bottomTabs = new Grid()
        {
            Padding = 10,
            ColumnSpacing = 10,
            ColumnDefinitions =
            [
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
            ],
            Children =
            {
                new MaterialIconButton(MaterialIconsRound.Mouse).Column(0),
                new MaterialIconButton(MaterialIconsRound.Keyboard_alt).Column(1),
                new MaterialIconButton(MaterialIconsRound.Sports_esports).Column(2),
                new MaterialIconButton(MaterialIconsRound.Play_circle).Column(3),
            }
        };

        Content = new Grid
        {
            RowSpacing = 1,
            RowDefinitions =
            [
                new RowDefinition { Height = new GridLength(9, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
            ],
            Children =
            {
                currentView.Row(0),
                bottomTabs.Row(1)
            }
        };
    }
}