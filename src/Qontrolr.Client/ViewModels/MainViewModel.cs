using Qontrolr.Client.Views.Common.Popups;
using System.Text.Json;

namespace Qontrolr.Client.ViewModels;

public partial class MainViewModel: ViewModel
{
    //Fields
    private readonly ClientSocketService _clientSocketService;

    //Construct
    public MainViewModel(ClientSocketService clientSocket)
    {
        _clientSocketService = clientSocket;

        InitializeViewModel();
    }

    private void InitializeViewModel()
    {
        _clientSocketService.OnConnected += async () =>
            await HandleConnectionEventAsync("Connected", "Successfully connected to server");

        _clientSocketService.OnConnectedError += async (ex) =>
            await HandleConnectionErrorAsync("Connection Error", ex.Message);

        _clientSocketService.OnDisconnected += async () =>
            await HandleConnectionErrorAsync("Disconnected", "Connection to server has been closed");

        _clientSocketService.OnSendError += async (ex) =>
            await HandleConnectionErrorAsync("Send Error", ex.Message);
    }

    private async Task HandleConnectionEventAsync(string title, string message)
    {
        await PopupService.ShowAlertAsync(title, message);
    }

    private async Task HandleConnectionErrorAsync(string title, string message)
    {
        await PopupService.ShowAlertAsync(title, message);
        await ConnectToServerAsync();
    }

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
        var connectedDevices = new List<string> { "Device 1", "Device 2", "Device 3" };
        var deviceListPopup = new ConnectedDevicesPopup(connectedDevices);
        var devicePopupResult = await PopupService.ShowPopupAsync(deviceListPopup);

        if (devicePopupResult == null)
        {
            var barcodeScanner = new BarcodeScannerPopup();
            var scannerResult = await PopupService.ShowPopupAsync(barcodeScanner);
            if (scannerResult != null)
            {
                await _clientSocketService.ConnectAsync((string)scannerResult);
                FireViewModelBusy();
            }
        }
        else
        {
            FireViewModelBusy();
            await _clientSocketService.ConnectAsync((string)devicePopupResult);
        }

        FireViewModelNotBusy();
    }
}