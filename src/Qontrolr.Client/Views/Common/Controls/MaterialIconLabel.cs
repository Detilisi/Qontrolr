using Qontrolr.Client.Views.Common.Fonts;

namespace Qontrolr.Client.Views.Common.Controls;

internal class MaterialIconLabel : Label
{
    public MaterialIconLabel(string iconName)
    {
        FontSize = 32;
        Text = iconName;
        FontFamily = MaterialIconsRound.FontFamily;

        VerticalOptions = LayoutOptions.Center;
        HorizontalOptions = LayoutOptions.Center;
    }
}
