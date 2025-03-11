using System.Text.Json;

namespace Qontrolr.Client.ViewModels;

public partial class MainViewModel : ObservableObject
{
    //Fields
    private readonly ClientSocketService _webSocketService;

    //Construction
    protected MainViewModel(ClientSocketService webSocketService)
    {
        _webSocketService = webSocketService;
        //_webSocketService.ErrorOccurred += async (s, e) => { await RetryConnection(); };
    }

    //Methods
    public async Task SendDeviceEventAsync<T>(DeviceId device, string name, T data)
    {
        var deviceEvent = new DeviceEvent<T>(device, name, data);
        string jsonCommand = JsonSerializer.Serialize(deviceEvent);
        await _webSocketService.SendAsync(jsonCommand);
    }
}
