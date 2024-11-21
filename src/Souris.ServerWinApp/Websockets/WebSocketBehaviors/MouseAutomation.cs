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
        if (string.IsNullOrEmpty(commandData)) return;

        HandleCommand(commandData);
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
                var clickDirection = int.Parse(command.Data);
                if (clickDirection == 0)
                {
                    _inputSimulator.Mouse.RightButtonClick();
                }
                else if (clickDirection == 1)
                {
                    _inputSimulator.Mouse.LeftButtonClick();
                }
                break;
            default:
                break;
        }
    }
}
