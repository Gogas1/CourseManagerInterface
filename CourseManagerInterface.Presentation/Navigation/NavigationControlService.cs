using CourseManagerInterface.Presentation.MVVM.ViewModel;
using CourseManagerInterface.Presentation.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagerInterface.Presentation.Navigation
{
    public class NavigationControlService
    {
        #region Events

        public event Action OnDisconnectionNavigation;

        #endregion Events

        #region Fields

        private readonly NavigationService _navigationService;
        private readonly ClientHost _clientHost;

        #endregion Fields

        #region Ctors
        public NavigationControlService(NavigationService navigationService, ClientHost clientHost)
        {
            _navigationService = navigationService;
            _clientHost = clientHost;

            _clientHost.OnDisconnect += ProceedDisconnection;
        }
        #endregion Ctors

        #region Methods

        #region Public

        public void ProceedDisconnection()
        {
            _navigationService.NavigateTo<ConnectViewModel>();
            OnDisconnectionNavigation?.Invoke();
        }

        #endregion Public

        #region Private



        #endregion Private

        #endregion Methods

    }
}
