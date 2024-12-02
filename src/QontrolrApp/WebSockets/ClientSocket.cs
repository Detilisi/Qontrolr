﻿using Qontrolr.Shared.Common.Events;
using System.Text.Json;
using WebSocketSharp;

namespace QontrolrApp.WebSockets;

internal class ClientSocket
{
    //Constants
    private const int ServerPort = 7890;
    private const string ServerIP = "10.0.2.2";
    private const string ServiceId = "mouse-automation";
    public static string ServerUrl { get; set; }


    //Fields
    private readonly WebSocket _webSocket;

    //Construction
    public ClientSocket()
    {
        var serverRoute = $"{ServerUrl}/{ServiceId}";
        var clientRoute = ServerUrl;
        _webSocket = new WebSocket(serverRoute);

        InitializeClient();
    }

    //Initialization
    public void InitializeClient()
    {
        _webSocket.OnMessage += Client_OnMessage;
    }

    //Handlers
    private void Client_OnMessage(object? sender, MessageEventArgs e)
    {
        Console.WriteLine(e.Data);
    }

    //Public methods
    public void Connect() => _webSocket.Connect();
    public void Close() => _webSocket.Close();
    public void Send(string data) => _webSocket.Send(data);
    public void SendEvent<T>(DeviceEvent<T> deviceEvent)
    {
        string jsonCommand = JsonSerializer.Serialize(deviceEvent);
        _webSocket.Send(jsonCommand);
    }
}
