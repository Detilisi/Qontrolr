using Qontrolr.Client.Views.SubViews.MediaKeys.Controls;

namespace Qontrolr.Client.Views.SubViews.MediaKeys;

public class MediaPadView : ContentView
{
    //Fields
    private readonly MediaKeysViewModel _viewModel;

    //Construction
    public MediaPadView(MediaKeysViewModel viewModel)
    {
        _viewModel = viewModel;

        InitializeView();
    }

    //Initialize
    private void InitializeView()
    {
        Padding = 5;
        Content = new Frame
        {
            HasShadow = true,
            CornerRadius = 200,
            WidthRequest = 300,
            HeightRequest = 300,
            BackgroundColor = Colors.Black,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            Content = new MediaControlButtons(MediaButton_Clicked)
        };
    }

    //MouseButton event Hanlders
    private void MediaButton_Clicked(Button sender, EventArgs e)
    {
        switch (sender.ClassId)
        {
            case "pause":
                _viewModel.TogglePlayCommand.Execute(null);
                break;
            case "next":
                _viewModel.NextCommand.Execute(null);
                break;
            case "prev":
                _viewModel.PreviousCommand.Execute(null);
                break;
            case "vol_up":
                _viewModel.VolumnUpCommand.Execute(null);
                break;
            case "vol_down":
                _viewModel.VolumnDownCommand.Execute(null);
                break;
            default:
                break;
        }
    }
}
