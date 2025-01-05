namespace Qontrolr.Client.Views;

public class MainPage1 : TabbedPage
{
	public MainPage1()
	{

        // Tab 1
        var tab1 = new ContentPage
        {
            Title = "Tab 1",
            Content = new Label
            {
                Text = "Welcome to Tab 1!",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            }
        };

        // Tab 2
        var tab2 = new ContentPage
        {
            Title = "Tab 2",
            Content = new Label
            {
                Text = "Welcome to Tab 2!",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            }
        };

        // Adding Tabs to TabbedPage
        Children.Add(tab1);
        Children.Add(tab2);
    }
}