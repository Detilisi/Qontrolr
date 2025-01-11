using CommunityToolkit.Maui.Markup;
using Qontrolr.Client.ViewModels.MousePad;
using Qontrolr.Client.Views.Common.Controls;
using Qontrolr.Client.Views.Common.Fonts;
using Qontrolr.Client.Views.SubViews.MediaPad;
using Qontrolr.Client.Views.SubViews.MousePad;

namespace Qontrolr.Client.Views.MainViews;

public class MainPage : ContentPage
{
    //Contruction
    public MainPage()
    {
        //Set up Main view
        var bottomToolBar = CreateBottomToolBar();
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

    //Helper methods
    private Grid CreateBottomToolBar()
    {
        return new Grid()
        {
            Padding = 10,
            ColumnSpacing = 10,
            ColumnDefinitions =
            [
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
            ],
            Children =
            {
                CreateButton("mouse", MaterialIconsRound.Mouse).Column(0),
                CreateButton("keyboard", MaterialIconsRound.Keyboard_alt).Column(1),
                CreateButton("gamepad", MaterialIconsRound.Sports_esports).Column(2),
                CreateButton("media", MaterialIconsRound.Play_circle).Column(3),
            }
        };
    }

    private MaterialIconButton CreateButton
    (
        string buttonId,
        string buttonIcon
    )
    {
        var newButton = new MaterialIconButton(buttonIcon)
        {
            ClassId = buttonId
        };

        newButton.Clicked += NewButton_Clicked;
        return newButton;
    }

    //Event handlers
    private void NewButton_Clicked(object? sender, EventArgs e)
    {
        if (sender is not Button button) return;

        switch (button.ClassId)
        {
            case "mouse":
                CurrentView = new MousePadView(new MousePadViewModel());
                break;
            case "keyboard":
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