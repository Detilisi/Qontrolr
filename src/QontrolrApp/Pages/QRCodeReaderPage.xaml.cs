using QontrolrApp.WebSockets;
using ZXing.Net.Maui;

namespace QontrolrApp.Pages;

public partial class QRCodeReaderPage : ContentPage
{
	public QRCodeReaderPage()
	{
		InitializeComponent();
	}
    
    private void OnBarcodeDetected(object sender, BarcodeDetectionEventArgs e)
    {
        // Handle barcode detection
        var scannedText = e.Results.FirstOrDefault()?.Value;
        if (!string.IsNullOrEmpty(scannedText))
        {
            // Stop scanning
            barcodeReader.IsDetecting = false;
            
            Dispatcher.Dispatch(async () =>
            {
                // Navigate back or handle connection logic
                bool isContinue = await DisplayAlert("Scanned QR Code", $"Scanned QR: {scannedText}", "Continue", "Retry");
                if (isContinue)
                {
                    ClientSocket.ServerUrl = scannedText;

                    // Navigate to Main page
                    await Shell.Current.GoToAsync("//MainPage");
                }
                else
                {
                    // Resume scanning
                    barcodeReader.IsDetecting = true;
                }
            });
        }
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        // Cancel and navigate back or close the pageTouchPadPage
        //await Navigation.PopAsync();
        await Shell.Current.GoToAsync("//TouchPadPage");
    }
}