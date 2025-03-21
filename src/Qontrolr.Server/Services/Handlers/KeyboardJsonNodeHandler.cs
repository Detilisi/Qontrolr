using Qontrolr.SharedLib.KeyBoard.EventData;
using Qontrolr.SharedLib.MediaKeys.EventData;

namespace Qontrolr.Server.Services.Handlers;

public class KeyboardJsonNodeHandler : JsonNodeHandler
{
    public override void Handle(JsonNode jsonNode)
    {
        string eventName = jsonNode["EventName"]?.ToString() ?? string.Empty;
        if (string.IsNullOrEmpty(eventName)) return;

        switch (eventName)
        {
            case nameof(EventNames.KeyClicked):
                var keyPresedData = jsonNode["EventData"].Deserialize<WinButtonId>();

                var keyClick = keyPresedData switch
                {
                    WinButtonId.Alt => WindowsInput.Native.VirtualKeyCode.SLEEP,
                    WinButtonId.Win => WindowsInput.Native.VirtualKeyCode.RWIN,
                    WinButtonId.Tab => WindowsInput.Native.VirtualKeyCode.TAB,
                    WinButtonId.Shift => WindowsInput.Native.VirtualKeyCode.SHIFT,
                    WinButtonId.Ctrl => WindowsInput.Native.VirtualKeyCode.CONTROL,
                    WinButtonId.Esc => WindowsInput.Native.VirtualKeyCode.ESCAPE,
                    WinButtonId.Insrt => WindowsInput.Native.VirtualKeyCode.INSERT,
                    WinButtonId.PrtSc => WindowsInput.Native.VirtualKeyCode.PRINT,
                    //_ => 
                };

                InputSimulator.Keyboard.KeyPress(keyClick);

                break;
        }

        //InputSimulator.Keyboard.TextEntry(keyPresed);
    }
}
