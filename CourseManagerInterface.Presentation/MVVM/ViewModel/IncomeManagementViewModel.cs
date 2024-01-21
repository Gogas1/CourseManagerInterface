using CourseManagerInterface.Presentation.Core;
using CourseManagerInterface.Presentation.Models;
using CourseManagerInterface.Presentation.MVVM.ViewModel.Additional;
using CourseManagerInterface.Presentation.Navigation;
using CourseManagerInterface.Presentation.Requests;
using CourseManagerInterface.Presentation.Requests.List;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagerInterface.Presentation.MVVM.ViewModel
{
    public class IncomeManagementViewModel : Core.ViewModel
    {
        private readonly RequestsService _requestsService;
        private readonly NavigationService _navigationService;

        private AsyncObservableCollection<Additional.ProductRecord> _incomeProducts = new();
        public AsyncObservableCollection<Additional.ProductRecord> IncomeProducts
        {
            get => _incomeProducts;
            set
            {
                _incomeProducts = value;
                OnPropertyChanged(nameof(IncomeProducts));
            }
        }

        private AsyncObservableCollection<IncomeRecord> _incomes = new();
        public AsyncObservableCollection<IncomeRecord> Incomes
        {
            get => _incomes;
            set
            {
                _incomes = value;
                OnPropertyChanged(nameof(Incomes));
            }
        }

        private IncomeRecord? _selectedIncome;
        public IncomeRecord? SelectedIncome
        {
            get => _selectedIncome;
            set
            {
                _selectedIncome = value;
                OnPropertyChanged(nameof(SelectedIncome));
                _requestsService.MakeRequest<GetIncomeProductsRequest>(value?.Id ?? 0);
            }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        private bool _isSearching;
        public bool IsSearching
        {
            get => _isSearching;
            set
            {
                _isSearching = value;
                OnPropertyChanged(nameof(IsSearching));
            }
        }

        public RelayCommand SearchIncomesCommand { get; }
        public RelayCommand ToIncomeCreatingCommand { get; }

        public IncomeManagementViewModel(RequestsService requestsService, NavigationService navigationService)
        {
            StartDate = DateTime.Now.AddDays(-1).Date;
            EndDate = DateTime.Now.AddDays(1).Date.AddMicroseconds(-1);

            _requestsService = requestsService;
            _navigationService = navigationService;

            SearchIncomesCommand = new RelayCommand(args => StartDate < EndDate, SearchIncomes);
            ToIncomeCreatingCommand = new RelayCommand(args => true, args => _navigationService.NavigateTo<IncomeRegisterViewModel>());
        }

        public void ShowSearchResult(IEnumerable<Income> incomesFound)
        {
            IsSearching = false;
            Incomes.Clear();
            foreach (var income in incomesFound)
            {
                IncomeRecord newIncomeRecord = new IncomeRecord(income);
                Incomes.Add(newIncomeRecord);
            }
        }

        public void ShowIncomeProducts(IEnumerable<Product> incomeProducts)
        {
            IncomeProducts.Clear();
            foreach (var incomeProduct in incomeProducts)
            {
                Additional.ProductRecord newIncomeProductRecord = new Additional.ProductRecord
                {
                    Id = incomeProduct.Id,
                    Name = incomeProduct.Name,
                    Description = incomeProduct.Description,
                    Type = incomeProduct.Type,
                    Amount = incomeProduct.Amount,
                    Price = incomeProduct.Price
                };
                IncomeProducts.Add(newIncomeProductRecord);
            }
        }

        private void SearchIncomes(object args)
        {
            IncomesSearchRequestArguments requestArguments = new IncomesSearchRequestArguments()
            {
                StartDate = this.StartDate, 
                EndDate = this.EndDate
            };

            IsSearching = true;
            _requestsService.MakeRequest<IncomesSearchRequest>(requestArguments);
        }        
    }

    public class IncomeRecord : Core.ViewModel
    {
        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private DateTime _createdAt;
        public DateTime CreatedAt
        {
            get => _createdAt;
            set
            {
                _createdAt = value;
                OnPropertyChanged(nameof(CreatedAt));
            }
        }

        private string _supplier = string.Empty;
        public string Supplier
        {
            get => _supplier;
            set
            {
                _supplier = value;
                OnPropertyChanged(nameof(Supplier));
            }
        }

        private decimal _summ;
        public decimal Summ
        {
            get => _summ;
            set
            {
                _summ = value;
                OnPropertyChanged(nameof(Summ));
            }
        }

        public IncomeRecord(Income income)
        {
            Id = income.Id;
            CreatedAt = income.CreatedAt;
            Supplier = income.Supplier;
            Summ = income.Summ;
        }
    }
}
