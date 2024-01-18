using CourseManagerInterface.Presentation.Navigation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagerInterface.Presentation.MVVM.ViewModel
{
    public class MainViewModel : Core.ViewModel
    {
        public MenuViewModel MenuViewModel { get; set; }

        private NavigationService _navigation;
        public NavigationService Navigation 
        { 
            get => _navigation;
            set
            {
                _navigation = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel(NavigationService navigation, MenuViewModel menuViewModel)
        {
            Navigation = navigation;
            MenuViewModel = menuViewModel;

            Navigation.NavigateTo<ConnectViewModel>();
        }
    }
}
