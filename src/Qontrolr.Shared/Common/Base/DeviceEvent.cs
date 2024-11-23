namespace Qontrolr.Shared.Common.Base;

public abstract class DeviceEvent<T>(string name, T data)
{
    public required string Name { get; set; } = name;
    public required T Data { get; set; } = data;
}
