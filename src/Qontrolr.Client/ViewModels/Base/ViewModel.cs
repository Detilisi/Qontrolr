namespace Qontrolr.Client.ViewModels.Base;

public abstract partial class ViewModel: ObservableObject
{
    //Properties
    [ObservableProperty] 
    public bool isBusy;

    //State changers
    protected void FireViewModelBusy() => IsBusy = true;
    protected void FireViewModelNotBusy() => IsBusy = false;
}
