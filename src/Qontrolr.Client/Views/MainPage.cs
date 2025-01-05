using Qontrolr.Client.Views.Common.Fonts;

namespace Qontrolr.Client.Views;

public class MainPage : TabbedPage
{
	public MainPage()
	{

        // Tab 1
        var tab1 = new ContentPage
        {
            Title = "Tab 1",
            IconImageSource = new FontImageSource()
            {
                FontFamily = MaterialIconsRound.FontFamily,
                Glyph = MaterialIconsRound.Home,
                Color = Colors.Black
            }
        };

        // Tab 2
        var tab2 = new ContentPage
        {
            Title = "Tab 2",
            IconImageSource = new FontImageSource()
            {
                FontFamily = MaterialIconsRound.FontFamily,
                Glyph = MaterialIconsRound.Home,
                Color = Colors.Black
            }
        };
        // Tab 2
        var tab3 = new ContentPage
        {
            Title = "Tab 3",
            IconImageSource = new FontImageSource()
            {
                FontFamily = MaterialIconsRound.FontFamily,
                Glyph = MaterialIconsRound.Home,
                Color = Colors.Black
            }
        };


        // Adding Tabs to TabbedPage
        Children.Add(tab1);
        Children.Add(tab2);
        Children.Add(tab3);

    }
}