using SignalR_Client.Classes;
using System.Threading.Tasks;

namespace SignalR_Client
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            string hubUrl = "https://localhost:7196/temperatureHub";

            // Two IotDevices with 2 IDs
            var device1 = new IotDevice(hubUrl, "Device_001");
            var device2 = new IotDevice(hubUrl, "Device_002");

            // Initiate connections for every device
            await device1.InitializeConnectionAsync();
            await device2.InitializeConnectionAsync();

            //Start temperature-report.
            var task1 = device1.ReportTemperatureAsync();
            var task2 = device2.ReportTemperatureAsync();

            //Waiting for the devices
            await Task.WhenAll(task1, task2);
        }
    }
}
