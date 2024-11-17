using WebSocketSharp;
using WebSocketSharp.Server;

namespace Souris.Server.Websockets.Services;

class MouseAutomationWebSockertBehavior : WebSocketBehavior
{
    protected override void OnMessage(MessageEventArgs e)
    {
        base.OnMessage(e);

        var data = e.Data;
    }
}
