using SourisApp.WebSockets;

namespace SourisApp
{
    public partial class MainPage : ContentPage
    {

        private readonly ClientWebSocket _clientWebSocket;
        public MainPage()
        {
            _clientWebSocket = new ClientWebSocket();

            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            _clientWebSocket.Connect();

            _clientWebSocket.Send("Hello from maui");
        }
    }

}
