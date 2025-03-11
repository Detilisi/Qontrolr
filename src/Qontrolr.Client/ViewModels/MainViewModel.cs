using Qontrolr.Client.Views.Common.Popups;
using System.Text.Json;

namespace Qontrolr.Client.ViewModels;

public partial class MainViewModel : ObservableObject
{
    //Fields
    private readonly ClientSocketService _clientSocketService;

    //Construction
    public MainViewModel(ClientSocketService webSocketService)
    {
        _clientSocketService = webSocketService;
    }

    //Methods
    public async Task SendDeviceEventAsync<T>(DeviceId device, string name, T data)
    {
        var deviceEvent = new DeviceEvent<T>(device, name, data);
        string jsonCommand = JsonSerializer.Serialize(deviceEvent);
        await _clientSocketService.SendAsync(jsonCommand);
    }

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