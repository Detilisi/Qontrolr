using Qontrolr.Client.Views.Common.Popups;
using System.Text.Json;

namespace Qontrolr.Client.ViewModels;

public partial class MainViewModel(ClientSocketService webSocketService) : ViewModel
{
    //Fields
    private readonly ClientSocketService _clientSocketService = webSocketService;

    //Methods
    public async Task SendDeviceEvent<T>(DeviceEvent<T> deviceEvent)
    {
        string jsonCommand = JsonSerializer.Serialize(deviceEvent);
        await _clientSocketService.SendAsync(jsonCommand);
    }

    //Commands
    [RelayCommand]
    public async Task ConnectToServerAsync()
    {
        var connectedDevices = new List<string> { "ws://localhost:5000/", "Device 2", "Device 3" };
        var deviceListPopup = new ConnectedDevicesPopup(connectedDevices);
        var devicePopupResult = await PopupService.ShowPopupAsync(deviceListPopup);

        if (devicePopupResult == null)
        {
            var barcodeScanner = new BarcodeScannerPopup();
            var scannerResult = await PopupService.ShowPopupAsync(barcodeScanner);
            if (scannerResult != null)
            {
                await _clientSocketService.ConnectAsync((string)scannerResult);
            }
        }
        else
        {
            await _clientSocketService.ConnectAsync((string)devicePopupResult);
        }
    }
}