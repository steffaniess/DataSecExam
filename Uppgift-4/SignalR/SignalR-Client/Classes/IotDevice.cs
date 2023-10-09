using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SignalR_Client.Helpers;



namespace SignalR_Client.Classes
{
    public class IotDevice
    {
        private readonly string _hubUrl;
        private readonly Random _random = new Random();
        private HubConnection _hubConnection;


        public IotDevice(string hubUrl)
        {
            _hubUrl = hubUrl;
        }
        public async Task InitializeConnectionAsync()
        {
            try
            {
                _hubConnection = new HubConnectionBuilder()
                               .WithUrl(_hubUrl)
                                .ConfigureLogging(logging => {
                                    logging.AddConsole();
                                    logging.SetMinimumLevel(LogLevel.Debug);
                                })
                               .Build();

                //start connection to SignalR-hub
                await _hubConnection.StartAsync();
                Console.WriteLine("Successfully connected to hub.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting to hub: {ex.Message}");
            }
        }
        public async Task ReportTemperatureAsync()
        {
            if (_hubConnection == null || _hubConnection.State == HubConnectionState.Disconnected)
            {
                await InitializeConnectionAsync();
            }
             
            while (true) 
            {
                try
                {
                    var temperature = _random.Next(-20, 50);

                    //Crypting temperature before it is send
                    string encryptedTemperature = DpapiEncryption.Encrypt(temperature.ToString());

                    await _hubConnection.SendAsync("SendTemperature", encryptedTemperature);
                    Console.WriteLine($"Sent temperature: {temperature}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending temperature: {ex.Message}");
                }
                await Task.Delay(5000); //every 5s


            }
        }
    }
}
