using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using UDP_Client.Classes;

public class UDPClient
{
    private const int serverPort = 11000;
    private static readonly IPAddress serverIPAddress = IPAddress.Parse("172.20.201.160");

    public static void Main()
    {
        Console.WriteLine("Enter person details:");
        Console.Write("Name: ");
        string name = Console.ReadLine();

        Console.Write("Age: ");
        if (!int.TryParse(Console.ReadLine(), out int age) || age < 0 || age > 150)
        {
            Console.WriteLine("Invalid age. Enter a valid age between 0 and 150.");
            return;
        }

        Console.Write("Birth Year: ");
        if (!int.TryParse(Console.ReadLine(), out int birthYear) || birthYear < 1900)
        {
            Console.WriteLine("Invalid birth year. Enter a valid birth year (at least 1900).");
            return;
        }

        //Person object with validated data
        Person person = new Person
        {
            Name = name,
            Age = age,
            Birth = birthYear
        };

        //Serialize the Person object to JSON
        string jsonData = JsonConvert.SerializeObject(person);

        using (UdpClient client = new UdpClient())
        {
            try
            {
                IPEndPoint serverEndPoint = new IPEndPoint(serverIPAddress, serverPort);

                //Convert the JSON data to bytes and send it to the server
                byte[] data = Encoding.ASCII.GetBytes(jsonData);
                client.Send(data, data.Length, serverEndPoint);
                Console.WriteLine("Data sent to server.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error sending data: {e}");
            }
        }
    }
}

//    private const int listenPort = 11001;

//    private static void StartListener()
//    {
//        UdpClient listener = new UdpClient(listenPort);
//        IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);

//        try
//        {
//            while (true)
//            {
//                Console.WriteLine("Waiting for broadcast");
//                byte[] bytes = listener.Receive(ref groupEP);

//                Console.WriteLine($"Received broadcast from {groupEP} :");
//                Console.WriteLine($" {Encoding.ASCII.GetString(bytes, 0, bytes.Length)}");
//            }
//        }
//        catch (SocketException e)
//        {
//            Console.WriteLine(e);
//        }
//        finally
//        {
//            listener.Close();
//        }

//    }


//    public static void Main()
//    {
//        StartListener();
//    }
//}