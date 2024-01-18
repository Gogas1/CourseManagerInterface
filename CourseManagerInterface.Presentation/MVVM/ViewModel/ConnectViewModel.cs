using CourseManagerInterface.Presentation.Core;
using CourseManagerInterface.Presentation.Navigation;
using CourseManagerInterface.Presentation.Networking;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseManagerInterface.Presentation.MVVM.ViewModel
{
    public class ConnectViewModel : Core.ViewModel
    {
        #region Props

        private string _host;
        public string Host
        {
            get => _host;
            set
            {
                _host = value;
                OnPropertyChanged(nameof(Host));
            }
        }

        private string _port;
        public string Port
        {
            get => _port;
            set
            {
                _port = value;
                OnPropertyChanged(nameof(Port));                
            }
        }

        #endregion Props

        #region Fields

        private readonly IConfiguration configuration;
        private readonly ClientHost clientHost;
        private readonly NavigationService navigationService;

        #endregion Fields

        #region Commands

        public RelayCommand ConnectCommand { get; set; }

        #endregion Commands

        #region Ctors

        public ConnectViewModel(IConfiguration configuration, ClientHost clientHost, NavigationService navigationService)
        {
            this.configuration = configuration;
            this.clientHost = clientHost;
            this.navigationService = navigationService;
            this.clientHost.OnConnect += Connected;

            ConnectCommand = new RelayCommand(CanExecuteConnection, TryToConnect);

            var defaultClientSettings = configuration.GetSection("ServerConfig");
            Host = defaultClientSettings["DefaultHost"] ?? throw new Exception("Default host did not specified");
            Port = defaultClientSettings["DefaultPort"] ?? throw new Exception("Default port did not specified");
        }

        #endregion Ctors

        #region Methods

        #region Private
        private void TryToConnect(object obj)
        {
            var port = int.Parse(Port);
            clientHost.RunClient(Host, port);
        }

        private void Connected()
        {
            navigationService.NavigateTo<HomeViewModel>();
        }

        private void Disconnected()
        {

        }

        private bool CanExecuteConnection(object obj)
        {
            if (string.IsNullOrEmpty(Host) && string.IsNullOrEmpty(Port)) return false;

            try
            {
                int.Parse(Port);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion Private

        #endregion Methods
    }
}
