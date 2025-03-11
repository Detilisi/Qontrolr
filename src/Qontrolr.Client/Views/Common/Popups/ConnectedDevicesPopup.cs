using Qontrolr.Client.Views.Common.Controls;
using System.Collections.ObjectModel;

namespace Qontrolr.Client.Views.Common.Popups;

public partial class ConnectedDevicesPopup : Popup
{
    // View elements
    private ObservableCollection<string> ConnectedDevices { get; set; } = new();

    //Construction
    public ConnectedDevicesPopup(List<string> connectedDevices)
    {
        foreach (var device in connectedDevices)
        {
            ConnectedDevices.Add(device);
        }

        InitializePopup();
    }

    private void InitializePopup()
    {
        Content = new VerticalStackLayout
        {
            Spacing = 10,
            WidthRequest = 300,
            Padding = new Thickness(10),
            Children =
            {
                new Label
                {
                    FontSize = 20,
                    Text = "Connected Devices",
                    HorizontalOptions = LayoutOptions.Center
                },
                new CollectionView
                {
                    HeightRequest = 200,
                    SelectionMode = SelectionMode.Single,
                    ItemsSource = ConnectedDevices,
                    ItemTemplate =new ConnectedDeviceTemplate(),
                }.Invoke(collectionView => collectionView.SelectionChanged += HandleSelectionChanged),

                new Button
                {
                    TextColor = Colors.White,
                    BackgroundColor = Colors.Black,
                    Text = "Scan for New Device",
                    Command = new Command(()=>{  Close(null); })
                }
            }
        };
    }

    //Handlers
    private void HandleSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var selectedDevice = e.CurrentSelection.FirstOrDefault();
        if(selectedDevice != null)
        {
            Close(selectedDevice);
        }
    }
}

//Helpers
internal class ConnectedDeviceTemplate : DataTemplate
{
    public ConnectedDeviceTemplate() : base(GenerateView) { }

    private static HorizontalStackLayout GenerateView()
    {
        return new HorizontalStackLayout
        {
            Spacing = 10,
            Margin = new Thickness(5),
            Padding = new Thickness(10),
            BackgroundColor = Colors.Black,
            Children =
            {
                new MaterialIconLabel(MaterialIconsRound.Computer) 
                { 
                    TextColor = Colors.Azure
                },
                new Label
                {
                    TextColor = Colors.Azure,
                    BackgroundColor = Colors.Transparent,
                    VerticalTextAlignment = TextAlignment.Center
                }.Bind(Label.TextProperty, ".")
            }
        }; 
    }
}