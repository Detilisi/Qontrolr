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

    //Event handlers
    private async Task RetryConnection()
    {
        //Connect to device
        var deviceListPopup = new ConnectedDevicesPopup(new List<string> { "Device 1", "Device 2", "Device 3" });
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
    }
}
