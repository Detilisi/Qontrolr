using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;
namespace Qontrolr.Client.Views.MainViews.Popups;

public partial class BarcodeScannerPopup : Popup
{
    private bool _isProcessing;
    private CameraBarcodeReaderView? _cameraBarcodeReaderView;
    public BarcodeScannerPopup()
    {
        InitializePopup();
    }

    private void InitializePopup()
    {
        Size = new Size(300, 500);

        _cameraBarcodeReaderView = new CameraBarcodeReaderView
        {
            IsTorchOn = false,
            IsDetecting = true,
            WidthRequest = 250,
            HeightRequest = 350,
            Margin = new Thickness(10),
            CameraLocation = CameraLocation.Rear
        };

        _cameraBarcodeReaderView.BarcodesDetected += BarcodesDetected;

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
                _cameraBarcodeReaderView,
                new Button
                {
                    Text = "✕ Close",
                    BackgroundColor = Colors.Black,
                    TextColor = Colors.White,
                    FontSize = 14,
                    CornerRadius = 0,
                    HeightRequest = 35,
                    HorizontalOptions = LayoutOptions.Fill,
                    Command = new Command(() => { Close(null); })
                }
            }
        };
    }

    //Handlers
    private void BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        if (_isProcessing || e.Results.Length == 0) return;

        _isProcessing = true;

        try
        {
            var barcode = e.Results[0].Value;
            Close(barcode);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error detecting barcode: {ex.Message}");
        }
    }

    protected override async Task OnClosed(object? result, bool wasDismissedByTappingOutsideOfPopup, CancellationToken token = default)
    {

        if (_cameraBarcodeReaderView != null)
        {
            // Stop the camera and release resources
            _cameraBarcodeReaderView.IsDetecting = false;
            _cameraBarcodeReaderView.IsEnabled = false;
            _cameraBarcodeReaderView.IsTorchOn = false;
            _cameraBarcodeReaderView.BarcodesDetected -= BarcodesDetected;

            // Dispose the camera view to release resources
            _cameraBarcodeReaderView = null;
        }

        // Reset processing flag
        _isProcessing = false;

        await base.OnClosed(result, wasDismissedByTappingOutsideOfPopup, token);
    }

}
