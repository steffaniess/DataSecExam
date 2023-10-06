using SignalR_Client.Classes;

namespace SignalR_Client
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var device = new IotDevice("https://localhost:7078/temperatureHub");
            await device.ReportTemperatureAsync();
        }
    }
}