using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using UDP_Client.Classes;

public class UDPListener
{
    private const int listenPort = 11000;

    private static void StartListener()
    {
        UdpClient listener = new UdpClient(listenPort);
        IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);

        try
        {
            while (true)
            {
                Console.WriteLine("Waiting for broadcast");
                byte[] bytes = listener.Receive(ref groupEP);
                string jsonData = Encoding.ASCII.GetString(bytes, 0, bytes.Length);

                Person person = JsonConvert.DeserializeObject<Person>(jsonData);

                Console.WriteLine($"Received broadcast from {groupEP} :");
                Console.WriteLine($"Name: {person.Name}, Age: {person.Age}, Birth: {person.Birth}");
            }
        }
        catch (SocketException e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            listener.Close();
        }
    }

    public static void Main()
    {
        StartListener();
    }
}
