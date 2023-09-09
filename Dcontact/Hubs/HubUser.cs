using Microsoft.AspNetCore.SignalR;
using System.Diagnostics.Metrics;

namespace Dcontact.Hubs
{   
    public class HubUser:Hub
    {
        //public static long counter = 0;
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }


    }
}
