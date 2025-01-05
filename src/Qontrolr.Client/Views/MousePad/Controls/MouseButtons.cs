using CommunityToolkit.Maui.Markup;

namespace Qontrolr.Client.Views.MousePad.Controls;

internal class MouseButtons : Grid
{
    //Fields
    private Button LeftButton;
    private Button RightButton;
    private Button MiddleButton;

    public Command ClickCommand { get;  set; }
    public Command PressedCommand { get;  set; }
    public Command ReleasedCommand { get;  set; }
    
    //Construction
    public MouseButtons()
    {
        LeftButton = CreateButton();
        RightButton = CreateButton();
        MiddleButton = CreateButton();

        //Set up Grids
        Padding = 0;
        ColumnSpacing = 1;
        ColumnDefinitions =
        [
            new ColumnDefinition { Width = new GridLength(4, GridUnitType.Star) },
            new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) },
            new ColumnDefinition { Width = new GridLength(4, GridUnitType.Star) },
        ];

        Children.Add(LeftButton.Column(0));
        Children.Add(MiddleButton.Column(1));
        Children.Add(RightButton.Column(2));
    }

    //Helper methods
    private Button CreateButton()
    {
        var newButton = new Button()
        {
            CornerRadius = 0,
            BackgroundColor = Colors.Gray
        };

        newButton.Clicked += Button_Clicked;
        newButton.Pressed += Button_Pressed;
        newButton.Released += Button_Released;
        
        return newButton;
    }

    private string GetButtonId(Button button)
    {
        return button switch
        {
            _ when button == LeftButton => "L",
            _ when button == RightButton => "R",
            _ when button == MiddleButton => "M",
            _ => string.Empty
        };
    }

    //Handlders
    private void Button_Clicked(object? sender, EventArgs e)
    {
        if (sender is not Button button) return;

        var buttonId = GetButtonId(button);
        ClickCommand.Execute(buttonId);
    }

    private void Button_Pressed(object? sender, EventArgs e)
    {
        if (sender is not Button button) return;

        var buttonId = GetButtonId(button);
        PressedCommand.Execute(buttonId);
    }

    private void Button_Released(object? sender, EventArgs e)
    {
        if (sender is not Button button) return;

        var buttonId = GetButtonId(button);
        ReleasedCommand.Execute(buttonId);
    }
}
