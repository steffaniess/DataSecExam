using Microsoft.AspNetCore.SignalR;

namespace SignalR_Server
{
    public class TemperatureHub : Hub
    {
        public async Task SendTemperature(string encryptedTemperature)
        {
            //A console for debugging
            Console.WriteLine("Received temperature data.");
            var decryptedTemperature = DpapiEncryption.Decrypt(encryptedTemperature);
            await Clients.All.SendAsync("ReceiveTemperature", decryptedTemperature);
        }
    }
}