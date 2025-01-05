using Qontrolr.Client.Views;
using Qontrolr.Client.Views.MousePad.Pages;

namespace Qontrolr.Client;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new MousePadPage();
    }
}
