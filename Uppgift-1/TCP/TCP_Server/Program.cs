using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class TcpServerExample
{
    static void Main()
    {
        Int32 port = 7281;
        IPAddress localAddr = IPAddress.Parse("127.0.0.1");
        TcpListener server = new TcpListener(localAddr, port);
        server.Start();
        Console.WriteLine("Waiting for a connection...");

        using TcpClient client = server.AcceptTcpClient();
        Console.WriteLine("Connected!");

        using NetworkStream stream = client.GetStream();

        byte[] bytes = new byte[256];
        StringBuilder completeMessage = new StringBuilder();

        int i;
        while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
        {
            completeMessage.Append(Encoding.UTF8.GetString(bytes, 0, i));
        }

        Console.WriteLine("Received: {0}", completeMessage);

        string acknowledgment = "Data received!";
        byte[] msg = Encoding.UTF8.GetBytes(acknowledgment);
        stream.Write(msg, 0, msg.Length);
        Console.WriteLine("Acknowledgment sent");

        client.Close();
        Console.WriteLine("Client disconnected");
    }
}
