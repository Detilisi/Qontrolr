using Qontrolr.Client.Views.SubViews.Touchpad.Controls;
using System.Windows.Input;

namespace Qontrolr.Client.Views.SubViews.Touchpad;

public class TouchpadView : ContentView
{
    // Constants
    private const int MinimumInterval = 16;         // ~60fps
    private const double MinimumVelocity = 0.3;     // Dead zone threshold
    private const double AccelerationFactor = 2;  // Acceleration curve power

    // Fields
    private CursorVector _lastPosition = new(0, 0);
    private DateTime _lastUpdateTime = DateTime.Now;
    private readonly TouchpadViewModel _viewModel;

    // Construction
    public TouchpadView(TouchpadViewModel viewModel)
    {
        _viewModel = viewModel;
        InitializeView();
    }

    // Initialize
    private void InitializeView()
    {
        Padding = 5;
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
                new TrackPad(TrackPadPanUpdated, MouseWheelPanUpdated).Row(0),
                new ControlButtons(MouseButton_Clicked, MouseButton_Pressed, MouseButton_Released).Row(1)
            }
        };
    }

    // Mouse Button Event Handlers
    private void MouseButton_Clicked(Button sender, EventArgs e)
    {
        ExecuteButtonCommand(_viewModel.ClickMouseButtonCommand, sender.ClassId);
    }

    private void MouseButton_Pressed(Button sender, EventArgs e)
    {
        ExecuteButtonCommand(_viewModel.PressMouseButtonCommand, sender.ClassId);
    }

    private void MouseButton_Released(Button sender, EventArgs e)
    {
        ExecuteButtonCommand(_viewModel.ReleaseMouseButtonCommand, sender.ClassId);
    }

    private static void ExecuteButtonCommand(ICommand command, string buttonId)
    {
        switch (buttonId)
        {
            case "R":
                command.Execute(MouseButtonId.Right);
                break;
            case "M":
                command.Execute(MouseButtonId.Middle);
                break;
            case "L":
                command.Execute(MouseButtonId.Left);
                break;
        }
    }

    // Mouse Wheel Event Handler
    private void MouseWheelPanUpdated(Frame sender, PanUpdatedEventArgs e)
    {
        var scroll = (int)e.TotalY;
        if (scroll == 0) return;

        var scrollDirection = scroll > 0 ? ScrollDirection.Up : ScrollDirection.Down;
        _viewModel.ScrollMouseWheelCommand.Execute(scrollDirection);
    }

    // Track Pad Event Handler
    private void TrackPadPanUpdated(Frame sender, PanUpdatedEventArgs e)
    {
        var currentTime = DateTime.Now;
        var timeDelta = (currentTime - _lastUpdateTime).TotalMilliseconds;

        // Throttle updates (except on gesture completion)
        if (timeDelta < MinimumInterval && e.StatusType != GestureStatus.Completed)
        {
            return;
        }

        switch (e.StatusType)
        {
            case GestureStatus.Started:
                ResetPointerTracking(currentTime);
                break;

            case GestureStatus.Running:
                ProcessPointerMovement(e, currentTime, timeDelta);
                break;

            case GestureStatus.Completed:
                ResetPointerTracking();
                break;
        }
    }

    // Helper methods for trackpad processing
    private void ResetPointerTracking(DateTime? currentTime = null)
    {
        _lastPosition = new CursorVector(0, 0);
        if (currentTime.HasValue)
        {
            _lastUpdateTime = currentTime.Value;
        }
    }

    private void ProcessPointerMovement(PanUpdatedEventArgs e, DateTime currentTime, double timeDelta)
    {
        if (timeDelta <= 0) return;

        var currentPosition = new CursorVector((int)e.TotalX, (int)e.TotalY);

        // Calculate velocity components
        var deltaX = currentPosition.PosX - _lastPosition.PosX;
        var deltaY = currentPosition.PosY - _lastPosition.PosY;

        var velocityX = deltaX / timeDelta;
        var velocityY = deltaY / timeDelta;

        // Apply acceleration curve
        var acceleratedX = ApplyAcceleration(velocityX);
        var acceleratedY = ApplyAcceleration(velocityY);

        // Calculate accelerated movement
        var newDeltaX = (int)(acceleratedX * timeDelta);
        var newDeltaY = (int)(acceleratedY * timeDelta);

        // Send relative movement to view model
        var relativeMovement = new CursorVector(newDeltaX, newDeltaY);
        _viewModel.MoveCursorRelativeCommand.Execute(relativeMovement);

        // Update tracking state
        _lastPosition = currentPosition;
        _lastUpdateTime = currentTime;
    }

    private static double ApplyAcceleration(double velocity)
    {
        // Apply dead zone to filter tiny movements
        if (Math.Abs(velocity) < MinimumVelocity)
        {
            return 0;
        }

        // Apply non-linear acceleration
        var sign = Math.Sign(velocity);
        var magnitude = Math.Abs(velocity);
        return sign * Math.Pow(magnitude, AccelerationFactor);
    }
}
