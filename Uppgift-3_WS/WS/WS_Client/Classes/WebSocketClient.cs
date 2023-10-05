using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;


namespace WSClient.Classes
{
    public class WebSocketClient
    {
        static async Task Main()
        {
            var serverUri = new Uri("ws://localhost:8080");
            var webSocket = new ClientWebSocket();

            try
            {

                await webSocket.ConnectAsync(serverUri, CancellationToken.None);
                Console.WriteLine("Connected to the WebSocket server...");

                //Send data to server
                var messageData = new { message = "Hi from client", timestamp = DateTime.Now };
                var messageJson = Newtonsoft.Json.JsonConvert.SerializeObject(messageData);
                var messageBytes = Encoding.UTF8.GetBytes(messageJson);

                await SendWebSocketsMessage(webSocket, messageBytes);

                //Receive data from server
                while (webSocket.State == WebSocketState.Open)
                {
                    var buffer = new byte[1024];
                    var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        var receivedData = Encoding.UTF8.GetString(buffer, 0, result.Count);
                        Console.WriteLine($"Received {receivedData} from server");
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"WebSocket error: {ex.Message}");
            }
            finally
            {
                webSocket.Dispose();
            }

        }

        static async Task SendWebSocketsMessage(WebSocket webSocket, byte[] message)
        {
            await webSocket.SendAsync(new ArraySegment<byte>(message), WebSocketMessageType.Text, true, CancellationToken.None);
        }

    }
}
