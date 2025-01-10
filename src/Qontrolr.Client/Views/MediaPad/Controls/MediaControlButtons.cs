using CommunityToolkit.Maui.Markup;
using Qontrolr.Client.Views.Common.Controls;
using Qontrolr.Client.Views.Common.Fonts;

namespace Qontrolr.Client.Views.MediaPad.Controls;

internal class MediaControlButtons : Grid
{
    //Construction
    public MediaControlButtons(Action<Button, EventArgs> buttonsClicked)
    {
        //Initialize buttons
        var pauseButton = MediaControlButtons.CreateButton("pause", MaterialIconsRound.Pause, buttonsClicked);
        var nextButton = MediaControlButtons.CreateButton("next", MaterialIconsRound.Skip_next, buttonsClicked);
        var prevButton = MediaControlButtons.CreateButton("prev", MaterialIconsRound.Skip_previous, buttonsClicked);

        var muteButton = MediaControlButtons.CreateButton("mute", MaterialIconsRound.Volume_mute, buttonsClicked);
        var volumUpButton = MediaControlButtons.CreateButton("vol_up", MaterialIconsRound.Volume_up, buttonsClicked);
        var volumDownButton = MediaControlButtons.CreateButton("vol_down", MaterialIconsRound.Volume_down, buttonsClicked);


        //Set up Grid
        ColumnSpacing = 2;
        ColumnDefinitions =
        [
            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
        ];
        RowDefinitions =
        [
            new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
            new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
            new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
        ];

        Children.Add(prevButton.Column(0).Row(1));
        Children.Add(pauseButton.Column(1).Row(1));
        Children.Add(nextButton.Column(2).Row(1));
        
        Children.Add(volumUpButton.Column(1).Row(0));
        Children.Add(volumDownButton.Column(1).Row(2));

    }

    //Helper methods
    private static Button CreateButton
    (
        string buttonId,
        string buttonIcon,
        Action<Button, EventArgs> xButtonClicked
    )
    {
        var newButton = new MaterialIconButton(buttonIcon, Colors.White)
        {
            ClassId = buttonId
        };

        newButton.Clicked += (s, e) => xButtonClicked(newButton, e);
        return newButton;
    }

}
