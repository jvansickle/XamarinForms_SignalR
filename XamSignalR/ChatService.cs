using System;
using Microsoft.AspNetCore.SignalR.Client;
using Xamarin.Forms;
using XamSignalR.Contract;

namespace XamSignalR
{
    public class ChatService
    {
        public event EventHandler<Message> MessageReceived;

        readonly HubConnection _connection;

        public ChatService()
        {
            _connection = new HubConnectionBuilder().WithUrl("http://localhost:5000/hub").Build();
            _connection.StartAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Console.WriteLine("There was an error opening the connection:{0}", task.Exception.GetBaseException());
                }
                else
                {
                    Console.WriteLine("Connected");

                    _connection.On<Message>("messageReceived", (message) =>
                    {
                        MessageReceived?.Invoke(null, message);
                    });
                }
            });
        }

        public void SendMessage(Message message)
        {
            _connection.SendAsync("NewMessage", message).ContinueWith(task =>
            {
                if(task.IsFaulted)
                {
                    // There was an error sending
                    Application.Current.MainPage.DisplayAlert("Send failure", "Message failed to send", "OK");
                }
            });
        }
    }
}
