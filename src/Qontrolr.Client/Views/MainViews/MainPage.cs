using CommunityToolkit.Maui.Markup;
using Qontrolr.Client.ViewModels.MousePad;
using Qontrolr.Client.Views.Common.Controls;
using Qontrolr.Client.Views.MainViews.Controls;
using Qontrolr.Client.Views.SubViews.KeyPad;
using Qontrolr.Client.Views.SubViews.MediaPad;
using Qontrolr.Client.Views.SubViews.MousePad;

namespace Qontrolr.Client.Views.MainViews;

public class MainPage : ContentPage
{
    //Contruction
    public MainPage()
    {
        //Set up Main view
        var bottomToolBar = new BottomToolBar(ToolBarButton_Clicked);
        CurrentView = new MousePadView(new MousePadViewModel());

        Content = new Grid
        {
            RowSpacing = 1,
            RowDefinitions =
            [
                new RowDefinition { Height = new GridLength(9, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
            ],
            Children =
            {
                CurrentView.Row(0),
                bottomToolBar.Row(1)
            }
        };
    }

    //View elements
    private ContentView? _currentView;
    private ContentView CurrentView
    {
        get => _currentView ?? new();
        set
        {
            if (_currentView == value) return;

            _currentView = value;
            OnPropertyChanged(nameof(CurrentView));
            (Content as Grid)?.Children.RemoveAt(0); // Remove old view
            (Content as Grid)?.Children.Insert(0, _currentView); // Add new view
        }
    }

    //Event handlers
    private void ToolBarButton_Clicked(MaterialIconButton sender, EventArgs e)
    {
        if (sender is not Button button) return;

        switch (button.ClassId)
        {
            case "mouse":
                CurrentView = new MousePadView(new MousePadViewModel());
                break;
            case "keyboard":
                CurrentView = new KeyPadView();
                break;
            case "gamepad":
                break;
            case "media":
                CurrentView = new MediaPadView();
                break;
            default:
                break;
        }
    }
}