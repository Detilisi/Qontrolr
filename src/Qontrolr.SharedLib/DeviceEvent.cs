using Qontrolr.SharedLib.Common;

namespace Qontrolr.SharedLib;

public record DeviceEvent<T>(DeviceId Device, string EventName, T EventData);
