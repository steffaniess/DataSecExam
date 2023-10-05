using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;


namespace WSClient.Classes
{
    public class WebSocketServer
    {
        static async Task Main()
        {
            var httpListener = new HttpListener();
            httpListener.Prefixes.Add("http://localhost:8080/");
            httpListener.Start();

            Console.WriteLine("WebSocket server is running..");

            while (true)
            {
                var context = await httpListener.GetContextAsync();
                if (context.Request.IsWebSocketRequest)
                {
                    var webSocketContext = await context.AcceptWebSocketAsync(null);
                    var webSocket = webSocketContext.WebSocket;

                    await HandleWebSocketConnection(webSocket);
                }
                else
                {
                    context.Response.StatusCode = 400;
                    context.Response.Close();
                }
                static async Task HandleWebSocketConnection(WebSocket webSocket)
                {
                    try
                    {
                        while (webSocket.State == WebSocketState.Open)
                        {
                            var buffer = new byte[1024];
                            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                            if (result.MessageType == WebSocketMessageType.Text)
                            {
                                var receivedData = Encoding.UTF8.GetString(buffer, 0, result.Count);
                                Console.WriteLine($"Received: {receivedData}");

                                //Stimulate the server to answer with Json-data
                                var responseData = new { message = "Server response", timespan = DateTime.Now };
                                //var responseJson = Newtonsoft.Json.JsonConvert.SerializeObject(responseData);
                                var responseBytes = Encoding.UTF8.GetBytes(receivedData);

                                await webSocket.SendAsync(new ArraySegment<byte>(responseBytes), WebSocketMessageType.Text, true, CancellationToken.None);

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
            }
        }
    }
}
