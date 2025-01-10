using Qontrolr.Client.Views.MediaPad.Controls;

namespace Qontrolr.Client.Views.MediaPad;

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
            CornerRadius = 200,
            WidthRequest = 300,
            HeightRequest = 300,

            HasShadow = true,
            BackgroundColor = Colors.LightGray,
            
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
            case "R":
               
                break;
            case "M":
                
                break;
            case "L":
                
                break;
            default:
                break;
        }
    }
}
