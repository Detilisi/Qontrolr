using Qontrolr.Client.ViewModels;
using Qontrolr.Client.Views.Common.Controls;

public class MainPage : ContentPage
{
    // Fields
    private readonly MainViewModel _mainViewModel;
    private readonly ActivityIndicator _activityIndicator;

    // View elements
    private readonly KeyPadView _keyPadView;
    private readonly MediaPadView _mediaPadView;
    private readonly TouchpadView _mousePadView;

    private ContentView? _currentView;
    private ContentView CurrentView
    {
        get => _currentView ?? _mousePadView;
        set
        {
            if (_currentView == value) return;

            _currentView = value;
            OnPropertyChanged(nameof(CurrentView));

            // Update current View
            if (Content is Grid grid)
            {
                grid.Children.RemoveAt(0); // Remove old view
                grid.Children.Insert(0, _currentView); // Add new view
            }
        }
    }

    // Constructor
    public MainPage(MainViewModel mainViewModel, KeyPadView keyPadView, MediaPadView mediaPadView, TouchpadView mousePadView)
    {
        _mainViewModel = mainViewModel;

        _keyPadView = keyPadView;
        _mediaPadView = mediaPadView;
        _mousePadView = mousePadView;

        _activityIndicator = new ActivityIndicator
        {
            IsRunning = false,
            IsVisible = false,
            Color = Colors.Blue,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center
        };

        InitializeMainView();
        SetBindings();
    }

    private void InitializeMainView()
    {
        Content = new Grid
        {
            RowSpacing = 1,
            RowDefinitions =
            {
                new RowDefinition { Height = new GridLength(9, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
            },
            Children =
            {
                CurrentView.Row(0),
                new BottomToolBar(OnToolBarButtonClicked).Row(1),
                _activityIndicator // Add ActivityIndicator to the grid
            }
        };
    }

    private void SetBindings()
    {
        _activityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, new Binding(nameof(MainViewModel.IsBusy), source: _mainViewModel));
        _activityIndicator.SetBinding(ActivityIndicator.IsVisibleProperty, new Binding(nameof(MainViewModel.IsBusy), source: _mainViewModel));
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _mainViewModel.ConnectToServerCommand.Execute(null);
    }

    // Event handlers
    private void OnToolBarButtonClicked(MaterialIconButton sender, EventArgs e)
    {
        if (sender is not Button button) return;

        var viewMap = new Dictionary<string, ContentView>
        {
            { "mouse", _mousePadView },
            { "keyboard", _keyPadView },
            { "media", _mediaPadView }
        };

        if (viewMap.TryGetValue(button.ClassId, out var view))
        {
            CurrentView = view;
        }
    }
}
