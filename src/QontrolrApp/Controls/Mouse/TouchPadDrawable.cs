namespace QontrolrApp.Controls.Mouse;

public class TouchPadDrawable : IDrawable
{
    public static TouchPadDrawable Instance { get; } = new TouchPadDrawable();

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        // Add centered text
        canvas.FontSize = 18;
        canvas.FontColor = Colors.LightGray;
        canvas.DrawString("Touchpad Area", dirtyRect.Width / 2, dirtyRect.Height / 2, HorizontalAlignment.Center);
    }
}
