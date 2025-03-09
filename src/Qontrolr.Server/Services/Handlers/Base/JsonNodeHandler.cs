namespace Qontrolr.Server.Services.Handlers.Base;

public abstract class JsonNodeHandler
{
    public InputSimulator InputSimulator { get; } = new();
    public abstract void Handle(JsonNode jsonNode);
}
