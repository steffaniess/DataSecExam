using Newtonsoft.Json;
using System.Net.WebSockets;
using System.Text;

namespace WebSocketClientApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var serverUri = new Uri("ws://localhost:8080/");
            using (var client = new ClientWebSocket())
            {
                await client.ConnectAsync(serverUri, CancellationToken.None);
                Console.WriteLine("Connected to the WebSocket server.");

                var requestData = new { message = "Hello from the client!" };
                var requestJson = JsonConvert.SerializeObject(requestData);
                var buffer = Encoding.UTF8.GetBytes(requestJson);

                await client.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);

                buffer = new byte[1024];
                var response = await client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                var responseMessage = Encoding.UTF8.GetString(buffer, 0, response.Count);

                Console.WriteLine($"Received from server: {responseMessage}");
            }
        }
    }
}
