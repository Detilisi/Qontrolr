using Qontrolr.Server.Services;
using QRCoder;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Qontrolr.Server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Fields
        private readonly ServerSocketService _serverWebSocket;

        //Construction
        public MainWindow()
        {
            _serverWebSocket = new ServerSocketService();

            InitializeComponent();
            StartServerAndDisplayQrCode();
        }

        //Helper
        private void StartServerAndDisplayQrCode()
        {
            // UI update
            _serverWebSocket.Start();
            ConnectText.Text = _serverWebSocket.SeverUrl;
            QrCodeImage.Source = GenerateQRCode(_serverWebSocket.SeverUrl);
        }

        private static BitmapImage GenerateQRCode(string text)
        {
            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCode(qrCodeData);

            using var qrBitmap = qrCode.GetGraphic(20);
            using var stream = new MemoryStream();
            qrBitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);

            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.StreamSource = new MemoryStream(stream.ToArray());
            bitmapImage.EndInit();

            return bitmapImage;
        }
    }
}