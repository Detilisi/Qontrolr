using Qontrolr.Client.Views.Common.Fonts;

namespace Qontrolr.Client.Views.Common.Controls;

internal class MaterialIconButton : Button
{
    public int IconSize { get; set; } = 40;
    public Color IconColor { get; set; } = Colors.Black;

    public MaterialIconButton(string iconName)
    {
        BackgroundColor = Colors.Transparent;
        VerticalOptions = LayoutOptions.Center;
        HorizontalOptions = LayoutOptions.Center;
        
        ImageSource = new FontImageSource()
        {
            Glyph = iconName,
            Size = IconSize,
            Color = IconColor,
            FontFamily = MaterialIconsRound.FontFamily
        };
    }
}
