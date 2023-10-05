using System.Net.Sockets;

static void Connect()
{
    string server = "127.0.0.1";
    string message = "bajs";
    try
    {
        // Create a TcpClient.
        // Note, for this client to work you need to have a TcpServer
        // connected to the same address as specified by the server, port
        // combination.

        Int32 port = 7281;

        // Prefer a using declaration to ensure the instance is Disposed later.
        using TcpClient client = new TcpClient(server, port);

        // Translate the passed message into ASCII and store it as a Byte array
        Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

        // Get a client stream for reading and writing.
        NetworkStream stream = client.GetStream();

        // Send the message to the connected TcpServer.
        stream.Write(data, 0, data.Length);

        Console.WriteLine("Sent: {0}", message);

        // Receive the server response.

        // Buffer to store the response bytes.
        data = new Byte[256];

        // String to store the response ASCII representation.
        String responseData = String.Empty;

        // Read the first batch of the TcpServer response bytes.
        Int32 bytes = stream.Read(data, 0, data.Length);
        responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
        Console.WriteLine("Received: {0}", responseData);

        // Explicit close is not necessary since TcpClient.Dispose() will be
        // called automatically.
        // stream.Close();
        // client.Close();
    }
    catch (ArgumentNullException e)
    {
        Console.WriteLine("ArgumentNullException: {0}", e);
    }
    catch (SocketException e)
    {
        Console.WriteLine("SocketException: {0}", e);
    }

    Console.WriteLine("\n Press Enter to continue...");
    Console.Read();
}




//using Azure;
//using System.Net.Sockets;
//using System.Text;

//class Client
//{
//	static void Main()
//	{
//		try
//		{
//			//connecting to server 8080
//			TcpClient client = new TcpClient("127.0.0.1", 8080);
//			NetworkStream stream = client.GetStream();

//			//structur
//			string request = "{\"action\": \"get_data\", \"parameter\": \"value\"}";

//			//send data to server
//			byte[] data = Encoding.UTF8.GetBytes(request);
//			stream.Write(data, 0, data.Length);

//			//answer from serverbyte
//			byte[] buffer = new byte[1024];
//			int bytesRead = stream.Read(buffer, 0, buffer.Length);
//			string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

//			Console.WriteLine("Server answered" + response);

//			stream.Close();
//			client.Close();
//		}
//		catch (Exception ex)
//		{
//			Console.WriteLine("Error" + ex.Message);
//		}
//	}
//}
//}