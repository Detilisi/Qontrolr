using ZXing;
using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;

namespace QontrolrApp.Pages;

public partial class BarcodeReaderPage : ContentPage
{
	public BarcodeReaderPage()
	{
		InitializeComponent();
	}

    private void OnBarcodeDetected(object sender, BarcodeDetectionEventArgs e)
    {
        // Handle barcode detection
        var barcode = e.Results.FirstOrDefault()?.Value;
        if (barcode != null)
        {
            Dispatcher.Dispatch(async () =>
            {
                // Navigate back or handle connection logic
                await DisplayAlert("Barcode Detected", $"Scanned QR Code: {barcode}", "OK");
            });
        }
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        // Cancel and navigate back or close the page
        await Navigation.PopAsync();
    }
}