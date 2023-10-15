using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SignalR_Client.Helpers;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace SignalR_Client.Classes
{
    public class IotDevice
    {
        private readonly string _hubUrl;
        private readonly Random _random = new Random();
        private HubConnection _hubConnection;
        private readonly string _deviceId;


        public IotDevice(string hubUrl, string deviceId)
        {
            _hubUrl = hubUrl;
            _deviceId = deviceId;
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

                    var dto = new TemperatureDTO { DeviceId = _deviceId, Temperature = encryptedTemperature };

                    var jsonDto = JsonConvert.SerializeObject(dto);


                    await _hubConnection.SendAsync("SendTemperature", jsonDto);
                    Console.WriteLine($"Sent temperature from {_deviceId}: {temperature}");
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
