using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class TcpServerExample
{
    static void Main()
    {
        // Set the TcpListener on port 8080.
        Int32 port = 7281;
        IPAddress localAddr = IPAddress.Parse("127.0.0.1");

        // Create a TcpListener to accept client connections
        TcpListener server = new TcpListener(localAddr, port);
        server.Start();
        Console.WriteLine("Waiting for a connection...");

        // Perform a blocking call to accept requests.
        using TcpClient client = server.AcceptTcpClient();
        Console.WriteLine("Connected!");

        // Get a stream object for reading and writing
        using NetworkStream stream = client.GetStream();

        byte[] bytes = new byte[256];
        int i;

        // Loop to receive all the data sent by the client.
        while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
        {
            // Translate data bytes to an ASCII string.
            string data = Encoding.ASCII.GetString(bytes, 0, i);
            Console.WriteLine("Received: {0}", data);

            // Process the data sent by the client.
            data = data.ToUpper();

            byte[] msg = Encoding.ASCII.GetBytes(data);

            // Send back a response.
            stream.Write(msg, 0, msg.Length);
            Console.WriteLine("Sent: {0}", data);
        }

        // Shutdown and end connection
        client.Close();
        Console.WriteLine("Client disconnected");
    }
}
