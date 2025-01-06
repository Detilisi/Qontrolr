using CommunityToolkit.Maui.Markup;

namespace Qontrolr.Client.Views.MousePad.Controls;

internal class MouseButtons : Grid
{
    
    //Construction
    public MouseButtons()
    {
        var leftButton = CreateButton("L");
        var rightButton = CreateButton("R");
        var middleButton = CreateButton("M");

        //Set up Grids
        Padding = 0;
        ColumnSpacing = 1;
        ColumnDefinitions =
        [
            new ColumnDefinition { Width = new GridLength(4, GridUnitType.Star) },
            new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) },
            new ColumnDefinition { Width = new GridLength(4, GridUnitType.Star) },
        ];

        Children.Add(leftButton.Column(0));
        Children.Add(middleButton.Column(1));
        Children.Add(rightButton.Column(2));
    }

    //Helper methods
    private Button CreateButton(string buttonId)
    {
        var newButton = new Button()
        {
            ClassId = buttonId,
            CornerRadius = 0,
            BackgroundColor = Colors.Gray
        };

        newButton.Clicked += Button_Clicked;
        newButton.Pressed += Button_Pressed;
        newButton.Released += Button_Released;
        
        return newButton;
    }

    //Handlders
    private void Button_Clicked(object? sender, EventArgs e)
    {
        if (sender is not Button button) return;
    }

    private void Button_Pressed(object? sender, EventArgs e)
    {
        if (sender is not Button button) return;
    }

    private void Button_Released(object? sender, EventArgs e)
    {
        if (sender is not Button button) return;
    }
}
