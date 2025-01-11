using Qontrolr.Client.Views.Common.Fonts;

namespace Qontrolr.Client.Views.Common.Controls;

internal class MaterialIconButton : Button
{
    public int IconSize { get; set; } = 40;
    
    public MaterialIconButton(string iconName)
    {
        BackgroundColor = Colors.Transparent;
        VerticalOptions = LayoutOptions.Center;
        HorizontalOptions = LayoutOptions.Center;
        
        ImageSource = new FontImageSource()
        {
            Glyph = iconName,
            Size = IconSize,
            Color = Colors.Black,
            FontFamily = MaterialIconsRound.FontFamily
        };
    }
    public MaterialIconButton(string iconName, Color? iconColor)
    {
        BackgroundColor = Colors.Transparent;
        VerticalOptions = LayoutOptions.Center;
        HorizontalOptions = LayoutOptions.Center;

        ImageSource = new FontImageSource()
        {
            Glyph = iconName,
            Size = IconSize,
            Color = iconColor ?? Colors.Black,
            FontFamily = MaterialIconsRound.FontFamily
        };
    }
}
