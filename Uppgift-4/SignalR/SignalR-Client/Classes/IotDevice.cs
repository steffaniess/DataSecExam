using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(_hubUrl)
                .Build();
        }
        public async Task ReportTemperatureAsync()
        {
            if (_hubConnection == null)
            {
                await InitializeConnectionAsync();
            }
             
            while (true) 
            {
                var temperature = _random.Next(-20, 50);
                await _hubConnection.SendAsync("SendTemperature", temperature.ToString());
                await Task.Delay(5000); //varje rapport uppdateras var 5e min
            }
        }
    }
}
