﻿using Qontrolr.Server.Websockets;
using QRCoder;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Qontrolr.ServerWinApp
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
            StartServer();
        }

        //Helper
        private void StartServer()
        {
            // UI update
            _isServerRunning = true;
            _serverWebSocket.Start();
            QrCodeImage.Source = GenerateQRCode("randomLink");
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