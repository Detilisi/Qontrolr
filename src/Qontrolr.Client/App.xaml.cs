using Qontrolr.Client.Views;

namespace Qontrolr.Client;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new MainPage1();
    }
}
