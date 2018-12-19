using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace XamSignalR.WebApi.Hubs
{
    public class ChatHub : Hub
    {
        public async Task NewMessage(string username, string message)
        {
            await Clients.All.SendAsync("messageReceived", username, message);
        }
    }
}
