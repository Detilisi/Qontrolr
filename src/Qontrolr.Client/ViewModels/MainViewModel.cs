using Qontrolr.Client.Views.Common.Popups;
using System.Text.Json;

namespace Qontrolr.Client.ViewModels;

public partial class MainViewModel : ViewModel
{
    //Flags
    private bool _isConnected = false;
    private string _currentDevice = string.Empty;
    private string _reconnectMessage = string.Empty;
    private string _connectionStatus = "Disconnected";

    //Fields
    private readonly ClientSocketService _clientSocketService;

    //Properties
    public string ConnectionStatus
    {
        get => _connectionStatus;
        set => SetProperty(ref _connectionStatus, value);
    }

    public bool IsConnected
    {
        get => _isConnected;
        set => SetProperty(ref _isConnected, value);
    }

    public string CurrentDevice
    {
        get => _currentDevice;
        set => SetProperty(ref _currentDevice, value);
    }

    public string ReconnectMessage
    {
        get => _reconnectMessage;
        set => SetProperty(ref _reconnectMessage, value);
    }

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
                IsConnected = true;
                await HandleConnectionEventAsync("Connected", "Successfully connected to server");
            });
        };

        _clientSocketService.OnConnectedError += (ex) =>
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                IsConnected = false;
                await HandleConnectionErrorAsync("Connection Error", ex.Message);
            });
        };

        _clientSocketService.OnDisconnected += () =>
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                IsConnected = false;
                await HandleConnectionErrorAsync("Disconnected", "Connection to server has been closed");
            });
        };

        _clientSocketService.OnSendError += (ex) =>
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await HandleTransientErrorAsync("Send Error", ex.Message);
            });
        };

        _clientSocketService.OnConnectionStateChanged += (state) =>
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                UpdateConnectionStatus(state);
            });
        };

        _clientSocketService.OnReconnectAttempt += (attempt, maxAttempts) =>
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                ReconnectMessage = $"Reconnecting... Attempt {attempt}/{maxAttempts}";
            });
        };
    }

    private void UpdateConnectionStatus(ConnectionState state)
    {
        switch (state)
        {
            case ConnectionState.Connected:
                ConnectionStatus = "Connected";
                IsConnected = true;
                ReconnectMessage = string.Empty;
                break;

            case ConnectionState.Connecting:
                ConnectionStatus = "Connecting...";
                IsConnected = false;
                break;

            case ConnectionState.Reconnecting:
                ConnectionStatus = "Connection lost";
                IsConnected = false;
                break;

            case ConnectionState.Failed:
                ConnectionStatus = "Connection failed";
                IsConnected = false;
                ReconnectMessage = "Could not reconnect. Please try again.";
                break;

            case ConnectionState.Disconnected:
                ConnectionStatus = "Disconnected";
                IsConnected = false;
                ReconnectMessage = string.Empty;
                break;
        }
    }

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

        // Only prompt to reconnect if we're not already trying to reconnect
        if (_clientSocketService.ConnectionState != ConnectionState.Reconnecting)
        {
            await ConnectToServerAsync();
        }
    }

    private async Task HandleTransientErrorAsync(string title, string message)
    {
        await PopupService.ShowAlertAsync(title, message);
    }

    //Methods
    public async Task SendDeviceEvent<T>(DeviceEvent<T> deviceEvent)
    {
        if (!_clientSocketService.IsConnected)
        {
            await PopupService.ShowAlertAsync("Not connected.", "Your action will be sent when connection is restored.");
            return;
        }

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
            var recentDevices = await GetRecentDevicesAsync();

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
                    await SaveRecentDeviceAsync(selectedDevice);
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

    // Add methods to save/retrieve recent devices (implement using your preferred storage method)
    private async Task<List<string>> GetRecentDevicesAsync()
    {
        // Example implementation - replace with your storage solution
        try
        {
            var recentJson = await SecureStorage.GetAsync("recent_devices");
            if (!string.IsNullOrEmpty(recentJson))
            {
                return JsonSerializer.Deserialize<List<string>>(recentJson) ?? [];
            }
        }
        catch { /* Handle storage errors */ }

        return [];
    }

    private async Task SaveRecentDeviceAsync(string device)
    {
        try
        {
            var recentDevices = await GetRecentDevicesAsync();

            // Remove if exists and add to beginning (most recent first)
            recentDevices.Remove(device);
            recentDevices.Insert(0, device);

            // Keep only the most recent 5 devices
            if (recentDevices.Count > 5)
            {
                recentDevices = recentDevices.Take(5).ToList();
            }

            await SecureStorage.SetAsync("recent_devices", JsonSerializer.Serialize(recentDevices));
        }
        catch { /* Handle storage errors */ }
    }
}