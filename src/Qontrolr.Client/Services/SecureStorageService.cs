namespace Qontrolr.Client.Services;

public static class SecureStorageService
{
    public static async Task<List<string>> GetRecentDevicesAsync()
    {
        try
        {
            var recentJson = await SecureStorage.GetAsync("recent_devices");
            if (!string.IsNullOrEmpty(recentJson))
            {
                return JsonSerializer.Deserialize<List<string>>(recentJson) ?? [];
            }
        }
        catch { /* Handle storage errors */ }

        return [];
    }

    public static async Task SaveRecentDeviceAsync(string device)
    {
        try
        {
            var recentDevices = await GetRecentDevicesAsync();

            // Remove if exists and add to beginning (most recent first)
            recentDevices.Remove(device);
            recentDevices.Insert(0, device);

            // Keep only the most recent 5 devices
            if (recentDevices.Count > 5)
            {
                recentDevices = recentDevices.Take(5).ToList();
            }

            await SecureStorage.SetAsync("recent_devices", JsonSerializer.Serialize(recentDevices));
        }
        catch { /* Handle storage errors */ }
    }
}
