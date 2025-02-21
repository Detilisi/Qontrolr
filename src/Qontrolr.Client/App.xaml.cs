using Qontrolr.Client.Views.MainViews;

namespace Qontrolr.Client;

public partial class App : Application
{
    public App(MainPage mainPage)
    {
        InitializeComponent();

        MainPage = mainPage;
    }
}
