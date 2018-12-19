using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using XamSignalR.Contract;

namespace XamSignalR
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        ObservableCollection<Message> _messages;
        public ObservableCollection<Message> Messages
        {
            get => _messages;
            private set
            {
                _messages = value;
                NotifyPropertyChanged();
            }
        }

        string _draft;
        public string Draft
        {
            get => _draft;
            set
            {
                _draft = value;
                NotifyPropertyChanged();
                Send.ChangeCanExecute();

            }
        }

        Command _send;
        public Command Send
        {
            get
            {
                if (_send == null)
                {
                    _send = new Command(OnSend, () => !string.IsNullOrWhiteSpace(Draft));
                }

                return _send;
            }
        }

        readonly string _username;
        readonly ChatService _chatService;

        public MainViewModel()
        {
            Messages = new ObservableCollection<Message>();
            _username = Guid.NewGuid().ToString();
            _chatService = new ChatService();
            _chatService.MessageReceived += OnMessageReceived;
        }

        void OnSend()
        {
            _chatService.SendMessage(new Message { Username = _username, Url = Draft });
            Draft = "";
        }

        void OnMessageReceived(object sender, Message message)
        {
            Messages.Add(message);
        }

        void NotifyPropertyChanged([CallerMemberName]string memberName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
        }
    }
}
