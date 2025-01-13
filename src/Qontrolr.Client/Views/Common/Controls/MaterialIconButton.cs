using Qontrolr.Client.Views.Common.Fonts;

namespace Qontrolr.Client.Views.Common.Controls;

internal class MaterialIconButton : Button
{
    public MaterialIconButton(string iconName, int iconSize = 40)
    {
        BackgroundColor = Colors.Transparent;
        VerticalOptions = LayoutOptions.Center;
        HorizontalOptions = LayoutOptions.Center;
        
        ImageSource = new FontImageSource()
        {
            Glyph = iconName,
            Size = iconSize,
            Color = Colors.Black,
            FontFamily = MaterialIconsRound.FontFamily
        };
    }
    public MaterialIconButton(string iconName, Color? iconColor, int iconSize = 40)
    {
        BackgroundColor = Colors.Transparent;

        ImageSource = new FontImageSource()
        {
            Glyph = iconName,
            Size = iconSize,
            Color = iconColor ?? Colors.Black,
            FontFamily = MaterialIconsRound.FontFamily
        };
    }
}
