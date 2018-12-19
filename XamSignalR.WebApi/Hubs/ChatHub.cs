using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using XamSignalR.Contract;

namespace XamSignalR.WebApi.Hubs
{
    public class ChatHub : Hub
    {
        public async Task NewMessage(Message message)
        {
            await Clients.All.SendAsync("messageReceived", message);
        }
    }
}
