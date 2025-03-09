using Qontrolr.SharedLib.MediaKeys.EventData;

namespace Qontrolr.Server.Services.Handlers;

public class MediaKeysJsonNodeHandler : JsonNodeHandler
{
    public override void Handle(JsonNode jsonNode)
    {
        string eventName = jsonNode["EventName"]?.ToString() ?? string.Empty;
        if (string.IsNullOrEmpty(eventName)) return;

        switch (eventName)
        {
            case nameof(EventNames.ButtonClicked):
                var mediaButtonClickedData = jsonNode["EventData"].Deserialize<MediaButtonId>();
                var mediaButtonClicked = mediaButtonClickedData switch
                {
                    MediaButtonId.Play => WindowsInput.Native.VirtualKeyCode.MEDIA_PLAY_PAUSE,
                    MediaButtonId.Next => WindowsInput.Native.VirtualKeyCode.MEDIA_NEXT_TRACK,
                    MediaButtonId.Prev => WindowsInput.Native.VirtualKeyCode.MEDIA_PREV_TRACK,
                    MediaButtonId.VolumnUp => WindowsInput.Native.VirtualKeyCode.VOLUME_UP,
                    MediaButtonId.VolumnDown => WindowsInput.Native.VirtualKeyCode.VOLUME_DOWN,
                    //_ => 
                };

                InputSimulator.Keyboard.KeyPress(mediaButtonClicked);
                break;
            default:
                break;
        }
    }
}
