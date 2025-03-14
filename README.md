# Qontrolr
Qontrolr transforms your Android device into a wireless mouse, keyboard, and media controller for your Windows PC. With a simple QR code scan, you can establish a connection between your devices and enjoy smooth, real-time control.

## Features
- Wireless Mouse Control – Move the cursor, click, scroll, and perform gestures.
- Wireless Keyboard – Type on your PC directly from your phone.
- Media Controls – Play, pause, adjust volume, and manage media playback.
- Quick QR Code Connection – Scan a QR code to pair your devices effortlessly.
- WebSocket Communication – Ensures fast and reliable real-time interaction.

## Tech Stack
- [.NET MAUI](https://github.com/dotnet/maui) – For the Android mobile client-application.
- [WPF](https://github.com/dotnet/wpf) – For the Windows server-application.
- [WebSocket-sharp](https://github.com/sta/websocket-sharp) – For real-time communication between devices.
- [Input Simulator Core](https://github.com/cwevers/InputSimulatorCore) – To simulate mouse and keyboard inputs on Windows.
- [QRCoder](https://github.com/codebude/QRCoder) - For generating QR code on server-app.
- [ZXing.Net.Maui](https://github.com/Redth/ZXing.Net.Maui) - For scanning QR code on client-app
