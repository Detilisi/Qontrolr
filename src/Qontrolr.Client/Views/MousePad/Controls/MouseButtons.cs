using CommunityToolkit.Maui.Markup;

namespace Qontrolr.Client.Views.MousePad.Controls;

internal class MouseButtons : Grid
{
    //Construction
    public MouseButtons
    (
        EventHandler XButtonClicked,
        EventHandler XButtonPressed,
        EventHandler XButtonReleased
    )
    {
        //Initialize buttons
        var leftButton = MouseButtons.CreateButton("L", XButtonClicked, XButtonPressed, XButtonReleased);
        var rightButton = MouseButtons.CreateButton("R", XButtonClicked, XButtonPressed, XButtonReleased);
        var middleButton = MouseButtons.CreateButton("M", XButtonClicked, XButtonPressed, XButtonReleased);

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
    private static Button CreateButton
    (
        string buttonId,
        EventHandler XButtonClicked,
        EventHandler XButtonPressed,
        EventHandler XButtonReleased
    )
    {
        var newButton = new Button()
        {
            ClassId = buttonId,
            CornerRadius = 0,
            BackgroundColor = Colors.Gray
        };

        newButton.Clicked += XButtonClicked;
        newButton.Pressed += XButtonPressed;
        newButton.Released += XButtonReleased;
        
        return newButton;
    }
}
