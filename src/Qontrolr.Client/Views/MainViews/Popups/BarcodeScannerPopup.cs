using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;
namespace Qontrolr.Client.Views.MainViews.Popups;

public partial class BarcodeScannerPopup : Popup
{
    private bool _isProcessing;

    public BarcodeScannerPopup()
    {
        InitializePopup();
    }

    private void InitializePopup()
    {
        Size = new Size(300, 500);

        var barcodeReaderView = new CameraBarcodeReaderView
        {
            IsTorchOn = false,
            IsDetecting = true,
            WidthRequest = 250,
            HeightRequest = 350,
            Margin = new Thickness(10),
            CameraLocation = CameraLocation.Rear,
        };
        barcodeReaderView.BarcodesDetected += BarcodesDetected;

        var closeButton = new Button
        {
            Text = "✕ Close",
            BackgroundColor = Colors.Black,
            TextColor = Colors.White,
            FontSize = 14,
            CornerRadius = 0,
            HeightRequest = 35,
            HorizontalOptions = LayoutOptions.Fill
        };
        closeButton.Clicked += (s, e) => { Close(null); };

        Content = new VerticalStackLayout
        {
            Spacing = 10,
            Padding = new Thickness(10),
            BackgroundColor = Colors.White,
            Children =
            {
                new Label
                {
                    Text = "Scan QR code to connect",
                    FontSize = 20,
                    HorizontalOptions = LayoutOptions.Center,
                    TextColor = Colors.Black,
                    Margin = new Thickness(0, 10, 0, 5)
                },
                barcodeReaderView,
                closeButton
            }
        };
    }

    //Handlers
    private async void BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        if (_isProcessing || e.Results.Length == 0) return;

        _isProcessing = true;
        try
        {
            var barcode = e.Results[0].Value;
            await Task.Delay(500);
            Close(barcode);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error detecting barcode: {ex.Message}");
        }
        finally
        {
            _isProcessing = false;
        }
    }
}
