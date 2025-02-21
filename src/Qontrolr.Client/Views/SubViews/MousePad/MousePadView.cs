using CommunityToolkit.Maui.Markup;
using Qontrolr.Client.ViewModels.MousePad;
using Qontrolr.Client.Views.SubViews.MousePad.Controls;
using Qontrolr.Shared.Mouse.Button.Enums;
using Qontrolr.Shared.Mouse.Cursor.ValueObjects;
using Qontrolr.Shared.Mouse.Wheel.Enums;

namespace Qontrolr.Client.Views.SubViews.MousePad;

internal class MousePadView : ContentView
{
    //Fields
    private readonly MousePadViewModel _viewModel;

    //Construction
    public MousePadView(MousePadViewModel viewModel)
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
                new TouchPad(TrackPadPanUpdated, MouseWheelPanUpdated).Row(0),
                new MouseButtons(MouseButton_Clicked, MouseButton_Pressed, MouseButton_Released).Row(1)
            }
        };
    }

    //MouseButton event Hanlders
    private void MouseButton_Clicked(Button sender, EventArgs e)
    {
        switch (sender.ClassId)
        {
            case "R":
                _viewModel.ClickMouseButtonCommand.Execute(ButtonId.Right);
                break;
            case "M":
                _viewModel.ClickMouseButtonCommand.Execute(ButtonId.Middle);
                break;
            case "L":
                _viewModel.ClickMouseButtonCommand.Execute(ButtonId.Left);
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
                _viewModel.PressMouseButtonCommand.Execute(ButtonId.Right);
                break;
            case "M":
                _viewModel.PressMouseButtonCommand.Execute(ButtonId.Middle);
                break;
            case "L":
                _viewModel.PressMouseButtonCommand.Execute(ButtonId.Left);
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
                _viewModel.ReleaseMouseButtonCommand.Execute(ButtonId.Right);
                break;
            case "M":
                _viewModel.ReleaseMouseButtonCommand.Execute(ButtonId.Middle);
                break;
            case "L":
                _viewModel.ReleaseMouseButtonCommand.Execute(ButtonId.Left);
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
        var newPosition = new CursorPosition((int)e.TotalX, (int)e.TotalY);
        _viewModel.DragMousePointerCommand.Execute(newPosition);
    }
}
