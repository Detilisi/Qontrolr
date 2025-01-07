using CommunityToolkit.Maui.Markup;

namespace Qontrolr.Client.Views.MousePad.Controls;

internal class MouseButtons : Grid
{
    //Construction
    public MouseButtons
    (
        Action<Button, EventArgs> buttonsClicked,
        Action<Button, EventArgs> buttonsPressed,
        Action<Button, EventArgs> buttonsReleased
    )
    {
        //Initialize buttons
        var leftButton = MouseButtons.CreateButton("L", buttonsClicked, buttonsPressed, buttonsReleased);
        var rightButton = MouseButtons.CreateButton("R", buttonsClicked, buttonsPressed, buttonsReleased);
        var middleButton = MouseButtons.CreateButton("M", buttonsClicked, buttonsPressed, buttonsReleased);

        //Set up Grid
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
        Action<Button, EventArgs> xButtonClicked,
        Action<Button, EventArgs> xButtonPressed,
        Action<Button, EventArgs> xButtonReleased
    )
    {
        var newButton = new Button()
        {
            ClassId = buttonId,
            CornerRadius = 0,
            BackgroundColor = Colors.Gray
        };

        newButton.Clicked   += (s, e) => xButtonClicked(newButton, e);
        newButton.Pressed   += (s, e) => xButtonPressed(newButton, e); 
        newButton.Released  += (s, e) => xButtonReleased(newButton, e); 
        
        return newButton;
    }
}
