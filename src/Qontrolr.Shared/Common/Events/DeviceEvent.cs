namespace Qontrolr.Shared.Common.Events;

public class DeviceEvent<T>(string name, T data)
{
    public string Name { get; } = name;
    public T Data { get; } = data;
}
