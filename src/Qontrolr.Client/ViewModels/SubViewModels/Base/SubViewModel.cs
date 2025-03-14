namespace Qontrolr.Client.ViewModels.SubViewModels.Base;

public abstract class SubViewModel(MainViewModel parentViewModel) : ViewModel
{
    //Fields
    protected MainViewModel _parentViewModel = parentViewModel;

    protected async Task SendDeviceEventAsync<T>(DeviceId device, string name, T data)
    {
        var deviceEvent = new DeviceEvent<T>(device, name, data);
        await _parentViewModel.SendDeviceEvent(deviceEvent);
    }
}
