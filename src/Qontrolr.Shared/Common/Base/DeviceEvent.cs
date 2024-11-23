namespace Qontrolr.Shared.Common.Base;

public class DeviceEvent<T>(string name, T data) where T : class
{
    public required string Name { get; set; } = name;
    public required T Data { get; set; } = data;
}
