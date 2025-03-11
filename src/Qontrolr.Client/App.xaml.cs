using Qontrolr.Client.Views;

namespace Qontrolr.Client;

public partial class App : Application
{
    public App(MainPage mainPage)
    {
        InitializeComponent();

        MainPage = mainPage;
    }
}
