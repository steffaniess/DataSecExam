using System;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using TCP_Client.Classes;

static void Connect()
{
    string server = "127.0.0.1";
    Int32 port = 7281;

    Car car = new Car
    {
        Brand = "Volkswagen",
        Model = "Up",
        Year = 2018
    };

    var jsonstring = JsonSerializer.Serialize(car);
    Console.WriteLine(jsonstring);

    try
    {
        using TcpClient client = new TcpClient(server, port);
        Byte[] data = Encoding.UTF8.GetBytes(jsonstring);

        using NetworkStream stream = client.GetStream();
        stream.Write(data, 0, data.Length);

        Console.WriteLine("Sent JSON data");

        data = new Byte[256];
        String responseData = String.Empty;

        Int32 bytes = stream.Read(data, 0, data.Length);
        responseData = Encoding.UTF8.GetString(data, 0, bytes);
        Console.WriteLine("Received: {0}", responseData);
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
