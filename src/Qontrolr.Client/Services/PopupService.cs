namespace Qontrolr.Client.Services;

public static class PopupService
{
    public static async Task ShowAlertAsync(string title, string message)
    {
        var page = Application.Current?.MainPage;
        if (page == null) return;

        await page.DisplayAlert(title, message, "OK");
    }

    public static async Task<object?> ShowPopupAsync(Popup popup)
    {
        var page = Application.Current?.MainPage;
        if (page == null) return null;

        return await page.ShowPopupAsync(popup);
    }
}
