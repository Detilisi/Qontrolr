namespace Qontrolr.Shared.Common.Base;

public abstract class DeviceEvent<T>(string name, T data)
{
    public  string Name { get;} = name;
    public T Data { get; } = data;
}
