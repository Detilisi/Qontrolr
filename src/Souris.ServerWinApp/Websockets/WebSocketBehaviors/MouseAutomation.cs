using Souris.Shared;
using System.Text.Json;
using WebSocketSharp;
using WebSocketSharp.Server;
using WindowsInput;

namespace Souris.Server.Websockets.WebSocketBehaviors;

class MouseAutomation : WebSocketBehavior
{
    private readonly InputSimulator _inputSimulator;

    public MouseAutomation()
    {
        _inputSimulator = new InputSimulator();
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        base.OnMessage(e);

        var commandData = e.Data;
        if (string.IsNullOrEmpty(commandData))
        {
            HandleCommand(commandData);
        }
    }

    public void HandleCommand(string jsonMessage)
    {
        var command = JsonSerializer.Deserialize<CommandModel>(jsonMessage);
        if (command == null) return;

        switch (command.Name)
        {
            case Commands.MoveCursor:
                var coordinates = command.Data.Split(',');
                int x = int.Parse(coordinates[0]);
                int y = int.Parse(coordinates[1]);

                _inputSimulator.Mouse.MoveMouseBy(x, y);
                break;

            case Commands.Click:
                if (command.Data == "right")
                {
                    _inputSimulator.Mouse.RightButtonClick();
                }
                else if (command.Data == "left")
                {
                    _inputSimulator.Mouse.RightButtonClick();
                }
                break;
            default:
                break;
        }
    }
}
