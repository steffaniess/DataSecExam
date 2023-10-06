using Microsoft.AspNetCore.SignalR;

namespace SignalR_Server
{
    public class TemperatureHub : Hub
    {
        public async Task SendTemperature(string temperature)
        {
            await Clients.All.SendAsync("ReceiveTemperature", temperature);
        }
    }
}
