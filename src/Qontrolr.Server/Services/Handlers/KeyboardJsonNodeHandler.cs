namespace Qontrolr.Server.Services.Handlers;

public class KeyboardJsonNodeHandler : JsonNodeHandler
{
    public override void Handle(JsonNode jsonNode)
    {
        string eventName = jsonNode["EventName"]?.ToString() ?? string.Empty;
        if (eventName != EventNames.ButtonClicked) return;

        var keyPresed = jsonNode["EventData"].Deserialize<string>();
        InputSimulator.Keyboard.TextEntry(keyPresed);
    }
}
