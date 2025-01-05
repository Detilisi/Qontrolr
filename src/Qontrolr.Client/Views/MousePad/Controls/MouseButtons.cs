using CommunityToolkit.Maui.Markup;
using System.Diagnostics;

namespace Qontrolr.Client.Views.MousePad.Controls;

internal class MouseButtons : Grid
{
    //Fields
    private Button LeftButton;
    private Button RightButton;
    private Button MiddleButton;
    
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

        newButton.Pressed += Button_Pressed;
        
        return newButton;
    }

    //Handlders
    private void Button_Pressed(object? sender, EventArgs e)
    {
        if (sender is not Button button) return;

        if (button == LeftButton) 
        {
            Debug.WriteLine("Left pressed");
        }
        else if (button == RightButton)
        {
            Debug.WriteLine("Right pressed");
        }
        else if (button == MiddleButton)
        {
            Debug.WriteLine("Mid pressed");
        }
    }

}
