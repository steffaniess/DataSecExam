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

        public IotDevice(string hubUrl)
        {
            _hubUrl = hubUrl;
        }
        public async Task ReportTemperatureAsync()
        {

        }
    }
}
