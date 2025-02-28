using Qontrolr.Client.Views.SubViews.Touchpad.Controls;

namespace Qontrolr.Client.Views.SubViews.Touchpad;

public class TouchpadView : ContentView
{
    //Fields
    private readonly TouchpadViewModel _viewModel;

    //Construction
    public TouchpadView(TouchpadViewModel viewModel)
    {
        _viewModel = viewModel;

        InitializeView();
    }

    //Initialize
    private void InitializeView()
    {
        Padding = 5;
        Content = new Grid()
        {
            RowSpacing = 1,
            RowDefinitions =
            [
                new RowDefinition { Height = new GridLength(9, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
            ],
            Children =
            {
                new TrackPad(TrackPadPanUpdated, MouseWheelPanUpdated).Row(0),
                new ControlButtons(MouseButton_Clicked, MouseButton_Pressed, MouseButton_Released).Row(1)
            }
        };
    }

    //MouseButton event Hanlders
    private void MouseButton_Clicked(Button sender, EventArgs e)
    {
        switch (sender.ClassId)
        {
            case "R":
                _viewModel.ClickMouseButtonCommand.Execute(MouseButtonId.Right);
                break;
            case "M":
                _viewModel.ClickMouseButtonCommand.Execute(MouseButtonId.Middle);
                break;
            case "L":
                _viewModel.ClickMouseButtonCommand.Execute(MouseButtonId.Left);
                break;
            default:
                break;
        }
    }

    private void MouseButton_Pressed(Button sender, EventArgs e)
    {
        switch (sender.ClassId)
        {
            case "R":
                _viewModel.PressMouseButtonCommand.Execute(MouseButtonId.Right);
                break;
            case "M":
                _viewModel.PressMouseButtonCommand.Execute(MouseButtonId.Middle);
                break;
            case "L":
                _viewModel.PressMouseButtonCommand.Execute(MouseButtonId.Left);
                break;
            default:
                break;
        }
    }

    private void MouseButton_Released(Button sender, EventArgs e)
    {
        switch (sender.ClassId)
        {
            case "R":
                _viewModel.ReleaseMouseButtonCommand.Execute(MouseButtonId.Right);
                break;
            case "M":
                _viewModel.ReleaseMouseButtonCommand.Execute(MouseButtonId.Middle);
                break;
            case "L":
                _viewModel.ReleaseMouseButtonCommand.Execute(MouseButtonId.Left);
                break;
            default:
                break;
        }
    }

    //MouseWheel event hanlders
    private void MouseWheelPanUpdated(Frame sender, PanUpdatedEventArgs e)
    {
        var scroll = (int)e.TotalY;

        if (scroll == 0) return;

        _viewModel.ScrollMouseWheelCommand.Execute(scroll > 0 ? ScrollDirection.Up : ScrollDirection.Down);
    }

    //MouseWheel event hanlders
    private void TrackPadPanUpdated(Frame sender, PanUpdatedEventArgs e)
    {
        var newPosition = new Vector2((int)e.TotalX, (int)e.TotalY);
        _viewModel.DragMousePointerCommand.Execute(newPosition);
    }
}
