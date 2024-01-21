using CourseManagerInterface.Presentation.Core;
using CourseManagerInterface.Presentation.Navigation;

namespace CourseManagerInterface.Presentation.MVVM.ViewModel
{
    public class HomeViewModel : Core.ViewModel
    {
        private readonly NavigationService _navigationService;

        public RelayCommand ProductsNavigateCommand { get; }
        public RelayCommand IncomesNavigateCommand { get; }
        public RelayCommand CreateOutgoingNavigateCommand { get; }
        public RelayCommand OutgoingsNavigateCommand { get; }

        public HomeViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;

            IncomesNavigateCommand = new RelayCommand(args => true, args => _navigationService.NavigateTo<IncomeManagementViewModel>());
            ProductsNavigateCommand = new RelayCommand(args => true, args => _navigationService.NavigateTo<ProductManagementViewModel>());
            CreateOutgoingNavigateCommand = new RelayCommand(args => true, args => _navigationService.NavigateTo<CreateOutgoingViewModel>());
            OutgoingsNavigateCommand = new RelayCommand(args => true, args => _navigationService.NavigateTo<OutgoingManagementViewModel>());
        }
    }
}
