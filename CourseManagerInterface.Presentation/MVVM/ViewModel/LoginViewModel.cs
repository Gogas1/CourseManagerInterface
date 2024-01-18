using CourseManagerInterface.Presentation.Core;
using CourseManagerInterface.Presentation.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagerInterface.Presentation.MVVM.ViewModel
{
    public class LoginViewModel : Core.ViewModel
    {
        private string _message;       
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        private readonly ClientHost clientHost;

        public RelayCommand SendMessageCommand { get; set; }

        public LoginViewModel(ClientHost clientHost)
        {
            this.clientHost = clientHost;

            SendMessageCommand = new RelayCommand(CanSendCommandBeExecuted, SendMessage);
        }

        private void SendMessage(object obj)
        {
            clientHost.SendMessage(Message);
        }

        private bool CanSendCommandBeExecuted(object obj)
        {
            return !string.IsNullOrEmpty(Message);
        }
    }
}
