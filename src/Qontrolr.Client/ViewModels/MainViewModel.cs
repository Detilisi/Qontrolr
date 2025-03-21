using Qontrolr.Client.Views.Common.Popups;

namespace Qontrolr.Client.ViewModels;

public partial class MainViewModel : ViewModel
{
    //Flags
    private string _currentDevice = string.Empty;
    
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
        _clientSocketService.OnConnected += () =>
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await HandleConnectionEventAsync("Connected", "Successfully connected to server");
            });
        };

        _clientSocketService.OnConnectedError += (ex) =>
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await HandleConnectionErrorAsync("Connection Error", ex.Message);
            });
        };

        _clientSocketService.OnDisconnected += () =>
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await HandleConnectionErrorAsync("Disconnected", "Connection to server has been closed");
            });
        };

        _clientSocketService.OnSendError += (ex) =>
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await HandleConnectionErrorAsync("Send Error", ex.Message);
            });
        };
    }

    //Methods
    public async Task SendDeviceEvent<T>(DeviceEvent<T> deviceEvent)
    {
        FireViewModelBusy();
        try
        {
            string jsonCommand = JsonSerializer.Serialize(deviceEvent);
            await _clientSocketService.SendAsync(jsonCommand);
        }
        finally
        {
            FireViewModelNotBusy();
        }
    }

    //Commands
    [RelayCommand]
    public async Task ConnectToServerAsync()
    {
        FireViewModelBusy();

        try
        {
            // Check for recent connections first
            var recentDevices = await SecureStorageService.GetRecentDevicesAsync();

            string? selectedDevice = null;

            if (recentDevices.Any())
            {
                var deviceListPopup = new ConnectedDevicesPopup(recentDevices);
                selectedDevice = await PopupService.ShowPopupAsync(deviceListPopup) as string;
            }

            if (selectedDevice == null)
            {
                var barcodeScanner = new BarcodeScannerPopup();
                selectedDevice = await PopupService.ShowPopupAsync(barcodeScanner) as string;

                if (selectedDevice != null)
                {
                    // Save to recent devices
                    await SecureStorageService.SaveRecentDeviceAsync(selectedDevice);
                }
            }

            if (selectedDevice != null)
            {
                _currentDevice = selectedDevice;
                await _clientSocketService.ConnectAsync(selectedDevice);
            }
        }
        finally
        {
            FireViewModelNotBusy();
        }
    }

    [RelayCommand]
    public async Task DisconnectAsync()
    {
        await _clientSocketService.CloseAsync();
        _currentDevice = string.Empty;
    }

    [RelayCommand]
    public async Task RetryConnectionAsync()
    {
        if (!string.IsNullOrEmpty(_currentDevice))
        {
            await _clientSocketService.ConnectAsync(_currentDevice);
        }
        else
        {
            await ConnectToServerAsync();
        }
    }

    //Event handlers
    private async Task HandleConnectionEventAsync(string title, string message)
    {
        if (title == "Connected" && !string.IsNullOrEmpty(_currentDevice))
        {
            await PopupService.ShowAlertAsync(title, $"Connected to {_currentDevice}");
        }
        else
        {
            await PopupService.ShowAlertAsync(title, message);
        }
    }

    private async Task HandleConnectionErrorAsync(string title, string message)
    {
        await PopupService.ShowAlertAsync(title, message);

        if (_clientSocketService.ConnectionState != ConnectionState.Reconnecting)
        {
            await ConnectToServerAsync();
        }
    }
}