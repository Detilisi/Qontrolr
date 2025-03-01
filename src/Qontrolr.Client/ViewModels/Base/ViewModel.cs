using Qontrolr.Client.Views.MainViews.Popups;

namespace Qontrolr.Client.ViewModels.Base;

public abstract partial class ViewModel: ObservableObject
{
    //Fields
    protected readonly WebSocketService _webSocketService;

    //Construction
    protected ViewModel(WebSocketService webSocketService)
    {
        _webSocketService = webSocketService;
        _webSocketService.ErrorOccurred += async (s, e) =>{ await RetryConnection(); };
    }

    //Helpers
    public async Task<object?> ShowPopupAsync(Popup popup)
    {
        var page = Application.Current?.MainPage;
        if (page == null) return null;

        return await page.ShowPopupAsync(popup);
    }

    public async Task ShowAlertAsync(string title, string message)
    {
        var page = Application.Current?.MainPage;
        if (page == null) return;
        await page.DisplayAlert(title, message, "OK");
    }

    //Event handlers
    private static bool _isRetrying = false;
    private async Task RetryConnection()
    {
        if (_isRetrying) return;
        _isRetrying = true;

        //Show error message
        await ShowAlertAsync("Connection Error", "An error occurred. Please try connecting to a server.");

        //Connect to device
        var deviceListPopup = new ConnectedDevicesPopup(["ws://localhost:5000/ws/", "Device 2", "Device 3"]);
        var devicePopupResult = await ShowPopupAsync(deviceListPopup);

        if (devicePopupResult == null)
        {
            var barcodeScanner = new BarcodeScannerPopup();
            var scannerResult = await ShowPopupAsync(barcodeScanner);

            if (scannerResult != null)
            {
                await _webSocketService.ConnectAsync((string)scannerResult);
            }
        }
        else
        {
            await _webSocketService.ConnectAsync((string)devicePopupResult);
        }

        _isRetrying = false;
    }
}
