using Souris.Shared;
using System.Text.Json;
using WebSocketSharp;
using WebSocketSharp.Server;
using WindowsInput;

namespace Souris.Server.Websockets.WebSocketBehaviors;

internal class MouseAutomation : WebSocketBehavior
{
    //Fields
    private readonly InputSimulator _inputSimulator;
    public static string Endpoint => "/mouse-automation";

    //Construction
    public MouseAutomation()
    {
        _inputSimulator = new InputSimulator();
    }

    //Event handlers
    protected override void OnMessage(MessageEventArgs e)
    {
        var message = e.Data;
        if (string.IsNullOrEmpty(message)) return;

        HandleCommand(message);
    }

    //Hepler
    private void HandleCommand(string jsonMessage)
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
