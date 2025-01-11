using Qontrolr.Client.Views.SubViews.MediaPad.Controls;

namespace Qontrolr.Client.Views.SubViews.MediaPad;

internal class MediaPadView : ContentView
{
    //Construction
    public MediaPadView()
    {
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
                break;
            case "next":
                break;
            case "prev":
                break;
            case "vol_up":
                break;
            case "vol_down":
                break;
            default:
                break;
        }
    }
}
