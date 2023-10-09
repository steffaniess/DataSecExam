using System;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebSocketServerApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            //Starta HTTP-Listener that listens to Websockets-requests
            var httpListener = new HttpListener();
            httpListener.Prefixes.Add("http://localhost:8080/");
            httpListener.Start();
            Console.WriteLine("WebSocket server started at ws://localhost:8080/");

            //Handle incoming Websockets-requests
            while (true)
            {
                var context = await httpListener.GetContextAsync();
                if (context.Request.IsWebSocketRequest)
                {
                    var webSocketContext = await context.AcceptWebSocketAsync(null);
                    HandleWebSocketConnection(webSocketContext.WebSocket);
                }
                else
                {
                    //If request is not Websoccket-request, send errorcode 400(Bad request)
                    context.Response.StatusCode = 400;
                    context.Response.Close();
                }
            }
        }

        private static async void HandleWebSocketConnection(WebSocket webSocket)
        {
            var buffer = new byte[1024];

            try
            {
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                Console.WriteLine($"Received: {message}");

                var responseData = new { greeting = "Hello from the server!", timestamp = DateTime.Now };
                var responseJson = JsonConvert.SerializeObject(responseData);
                buffer = Encoding.UTF8.GetBytes(responseJson);

                await webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            finally
            {
                webSocket.Dispose();
            }
        }
    }
}
