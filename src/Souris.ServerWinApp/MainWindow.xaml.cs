using Souris.Server.Websockets;
using System.Windows;

namespace Souris.ServerWinApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Fields
        private bool _isServerRunning = false;
        private readonly ServerSocket _serverWebSocket;

        //Construction
        public MainWindow()
        {
            _serverWebSocket = new ServerSocket();

            InitializeComponent();
        }

        //Event handler
        private void ServerButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isServerRunning)
            {
                StopServer();
            }
            else
            {
                StartServer();
            }
        }

        //Helper
        private void StartServer()
        {
            // UI update
            _isServerRunning = true;
            ServerButton.Content = "Stop Server";
            StatusMessage.Text = "Server has started successfully!";
            StatusMessage.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.LimeGreen);
            StatusMessage.Visibility = Visibility.Visible;

            _serverWebSocket.Start();
        }

        private void StopServer()
        {
            // UI update
            _isServerRunning = false;
            ServerButton.Content = "Start Server";
            StatusMessage.Text = "Server has been stopped!";
            StatusMessage.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
            StatusMessage.Visibility = Visibility.Visible;


            _serverWebSocket.Stop();
        }
    }
}