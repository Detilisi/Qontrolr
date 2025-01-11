using CommunityToolkit.Maui.Markup;

namespace Qontrolr.Client.Views.Common.Controls.Base;

internal abstract class CustomGridControl : Grid
{
    /// <summary>
    /// Initializes Grid properties and column definitions.
    /// Derived classes must override to provide specific definitions.
    /// </summary>
    protected abstract void InitializeGrid();

    /// <summary>
    /// Adds a view to the grid at the specified column and row.
    /// </summary>
    protected void AddToGrid(View view, int column = 0, int row = 0)
    {
        view.Column(column).Row(row);
        Children.Add(view);
    }

    /// <summary>
    /// Creates an icon button with common properties and event handlers.
    /// </summary>
    protected MaterialIconButton CreateIconButton
    (
        string buttonId,
        string buttonIcon,
        Color? iconColor = null,
        Action<MaterialIconButton, EventArgs>? clicked = null,
        Action<MaterialIconButton, EventArgs>? pressed = null,
        Action<MaterialIconButton, EventArgs>? released = null
    )
    {
        var button = new MaterialIconButton(buttonIcon, iconColor)
        {
            CornerRadius = 0,
            ClassId = buttonId,
            BackgroundColor = Colors.Transparent,
        };

        if (clicked != null)
            button.Clicked += (s, e) => clicked(button, e);
        if (pressed != null)
            button.Pressed += (s, e) => pressed(button, e);
        if (released != null)
            button.Released += (s, e) => released(button, e);

        return button;
    }


    /// <summary>
    /// Creates a frame with common properties and optional content.
    /// </summary>
    protected Frame CreateFrame
    (
        Action<Frame, PanUpdatedEventArgs> panUpdated,
        View? content = null,
        Color? backgroundColor = null,
        Color? borderColor = null
    )
    {
        var frame = new Frame
        {
            Padding = 0,
            CornerRadius = 0,
            BackgroundColor = backgroundColor ?? Colors.Black,
            BorderColor = borderColor ?? Colors.Transparent,
            Content = content ?? new Label() { Text = "Empty"}
        };

        var panGesture = new PanGestureRecognizer();
        panGesture.PanUpdated += (sender, e) => panUpdated(frame, e);
        frame.GestureRecognizers.Add(panGesture);

        return frame;
    }
}
