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
            double.TryParse(parts[0], out double deltaX) &&
            double.TryParse(parts[1], out double deltaY))
        {
            Debug.WriteLine((int)deltaX + ", " + (int)deltaY);
            _inputSimulator.Mouse.MoveMouseBy((int)deltaX, (int)deltaY);
        }
    }
}
