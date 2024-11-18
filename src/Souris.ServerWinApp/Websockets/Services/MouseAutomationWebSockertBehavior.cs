using System.Diagnostics;
using WebSocketSharp;
using WebSocketSharp.Server;
using WindowsInput;

namespace Souris.Server.Websockets.Services;

class MouseAutomationWebSockertBehavior : WebSocketBehavior
{
    private readonly InputSimulator _inputSimulator;

    public MouseAutomationWebSockertBehavior()
    {
        _inputSimulator = new InputSimulator();
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        base.OnMessage(e);

        var parts = e.Data.Split(",");

        if (parts.Length == 2 &&
            int.TryParse(parts[0], out int deltaX) &&
            int.TryParse(parts[1], out int deltaY))
        {
            _inputSimulator.Mouse.MoveMouseBy(deltaX, deltaY);
        }
    }
}
