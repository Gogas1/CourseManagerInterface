using CourseManagerInterface.Presentation.Core;
using CourseManagerInterface.Presentation.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagerInterface.Presentation.MVVM.ViewModel
{
    public class HomeViewModel : Core.ViewModel
    {
        private readonly NavigationService _navigationService;

        public RelayCommand ProductsNavigateCommand { get; }
        public RelayCommand IncomesNavigateCommand { get; }

        public HomeViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;

            IncomesNavigateCommand = new RelayCommand(args => true, args => _navigationService.NavigateTo<IncomeManagementViewModel>());
            ProductsNavigateCommand = new RelayCommand(args => true, args => _navigationService.NavigateTo<ProductManagementViewModel>());
        }              
    }
}
