using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using SignalR_Client.Classes;
using SignalR_Client.Helpers;

namespace SignalR_Server
{
    public class TemperatureHub : Hub
    {
        public async Task SendTemperature(string jsonDto)
        {
            var dto = JsonConvert.DeserializeObject<TemperatureDTO>(jsonDto);
            //A console for debugging
            Console.WriteLine("Received temperature data.");
            var decryptedTemperature = DpapiEncryption.Decrypt(dto.Temperature);
            await Clients.All.SendAsync("ReceiveTemperature", dto.DeviceId, decryptedTemperature);
        }
    }
}