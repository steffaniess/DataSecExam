using SignalR_Client.Classes;

namespace SignalR_Client
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var device = new IotDevice("https://localhost:7078/swagger/index.html");
            await device.ReportTemperatureAsync();
        }
    }
}