using CourseManagerInterface.Presentation.Core;
using CourseManagerInterface.Presentation.Navigation;
using CourseManagerInterface.Presentation.Networking;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagerInterface.Presentation.MVVM.ViewModel
{
    public class MenuViewModel : Core.ViewModel
    {
        #region Commands

        public RelayCommand DisconnectCommand { get; }
        public RelayCommand ConnectCommand { get; }
        public RelayCommand ExitCommand { get; }

        public RelayCommand NavigateToIncomeRegistraionViewCommand { get; }

        public RelayCommand NavigateToHomeCommand { get; }

        #endregion Commands

        #region Fields

        private readonly ClientHost _clientHost;
        private readonly NavigationService _navigationService;
        private readonly MainWindow _mainWindow;

        private bool _isConnected = false;
        public bool IsConnected
        {
            get => _isConnected;
            set
            {
                _isConnected = value;
                OnPropertyChanged(nameof(IsConnected));
            }
        }

        #endregion Fields

        #region Ctors

        public MenuViewModel(ClientHost clientHost, NavigationService navigationService)
        {
            _clientHost = clientHost;
            _navigationService = navigationService;

            DisconnectCommand = new RelayCommand(args => _isConnected, args => _clientHost.Disconnect());
            ConnectCommand = new RelayCommand(args => !_isConnected, args => _navigationService.NavigateTo<ConnectViewModel>());
            ExitCommand = new RelayCommand(args => true, Exit);

            NavigateToIncomeRegistraionViewCommand = new RelayCommand(args => _isConnected, args => _navigationService.NavigateTo<IncomeRegisterViewModel>());
            NavigateToHomeCommand = new RelayCommand(args => _isConnected, args => _navigationService.NavigateTo<HomeViewModel>());

            _clientHost.OnDisconnect += OnDisconnection;
            _clientHost.OnConnect += OnConnection;
        }

        #endregion Ctors

        #region Methods

        #region Public


        #endregion Public

        #region Private
        
        private void Exit(object args)
        {
            _clientHost.CloseClient();
            _mainWindow.Close();
        }

        private void OnConnection()
        {
            _isConnected = true;
        }

        private void OnDisconnection()
        {
            _isConnected = false;
        }

        #endregion Private

        #endregion Methods
    }
}
