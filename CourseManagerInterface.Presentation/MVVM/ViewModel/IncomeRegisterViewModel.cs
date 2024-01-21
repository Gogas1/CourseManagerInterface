using AutoMapper;
using CourseManagerInterface.Presentation.Commands.List;
using CourseManagerInterface.Presentation.Core;
using CourseManagerInterface.Presentation.Dialogues;
using CourseManagerInterface.Presentation.MVVM.View.Dialogue;
using CourseManagerInterface.Presentation.MVVM.ViewModel.Dialogue;
using CourseManagerInterface.Presentation.Navigation;
using CourseManagerInterface.Presentation.Requests;
using CourseManagerInterface.Presentation.Requests.List;
using System.Collections.ObjectModel;

namespace CourseManagerInterface.Presentation.MVVM.ViewModel
{
    public class IncomeRegisterViewModel : Core.ViewModel
    {
        private ObservableCollection<Additional.ProductRecord> _incomeProducts = new ObservableCollection<Additional.ProductRecord>();
        public ObservableCollection<Additional.ProductRecord> IncomeProducts
        {
            get => _incomeProducts;
            set
            {
                _incomeProducts = value;
                OnPropertyChanged(nameof(IncomeProducts));
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

        private bool _isSubmiting;
        public bool IsSubmitting
        {
            get => _isSubmiting;
            set
            {
                _isSubmiting = value;
                OnPropertyChanged(nameof(IsSubmitting));
            }
        }
        private bool _isSubmitted;
        public bool IsSubmitted
        {
            get => _isSubmitted;
            set
            {
                _isSubmitted = value;
                OnPropertyChanged(nameof(IsSubmitted));
            }
        }
        private bool _isCanceled;
        public bool IsCanceled
        {
            get => _isCanceled;
            set
            {
                _isCanceled = value;
                OnPropertyChanged(nameof(IsCanceled));
            }
        }

        private readonly DialogueService _dialogueService;
        private readonly RequestsService _requestsService;
        private readonly NavigationService _navigationService;
        private readonly IMapper _mapper;
        private readonly AddIncomeProductFoundCommandCallbackService _productFoundCallbackService;

        public RelayCommand ShowAddProductDialogCommand { get; }
        public RelayCommand SendCreationRequestCommand { get; }
        public RelayCommand HomeNavigationCommand { get; }

        public IncomeRegisterViewModel(DialogueService dialogueService, RequestsService requestsService, IMapper mapper, AddIncomeProductFoundCommandCallbackService productFoundCallbackService, NavigationService navigationService)
        {
            _dialogueService = dialogueService;
            _requestsService = requestsService;
            _mapper = mapper;
            _productFoundCallbackService = productFoundCallbackService;
            _navigationService = navigationService;

            ShowAddProductDialogCommand = new RelayCommand(args => true, ShowDialogAction);
            SendCreationRequestCommand = new RelayCommand(args => IncomeProducts.Any(), Create);
            HomeNavigationCommand = new RelayCommand(args => true, args => _navigationService.NavigateTo<HomeViewModel>());
        }

        private void ShowDialogAction(object args)
        {
            ShowDialog(viewModel => _dialogueService.ShowDialog<AddIncomeProductDialog>(viewModel));
        }

        private void ShowDialog(Func<AddIncomeProductDialogViewModel, bool> showDialog)
        {
            var dialogueViewModel = new AddIncomeProductDialogViewModel(_productFoundCallbackService, _requestsService);

            bool success = showDialog(dialogueViewModel);
            if (success)
            {
                int id;
                int.TryParse(dialogueViewModel.Id, out id);
                string name = dialogueViewModel.Name;
                string? description = dialogueViewModel.Description;
                double count = double.Parse(dialogueViewModel.Count);
                decimal price = decimal.Parse(dialogueViewModel.Price);
                string type = dialogueViewModel.Type;

                AddProductToList(id, name, description, count, price, type);
            }
            dialogueViewModel.UnsubscribeEvents();
        }

        private void AddProductToList(int id, string name, string? description, double count, decimal price, string type)
        {
            Additional.ProductRecord newProductRecord = new Additional.ProductRecord()
            {
                Id = id,
                Name = name,
                Description = description ?? string.Empty,
                Price = price,
                Amount = count,
                Type = type
            };

            IncomeProducts.Add(newProductRecord);
        }

        private void Create(object args)
        {
            IsSubmitting = true;
            var products = _mapper.Map<List<RequestIncomeProduct>>(IncomeProducts);

            AddIncomeRequestArguments requestArguments = new AddIncomeRequestArguments()
            {
                DateTime = DateTime.Now,
                Supplier = Supplier,
                Products = products
            };

            _requestsService.MakeRequest<AddIncomeRequest>(requestArguments);
        }

        public void SetSubmitResult(bool result)
        {
            IsSubmitting = false;
            IsSubmitted = result;
            IsCanceled = !result;

            if (IsSubmitted)
            {
                Reset();
            }
        }

        private void Reset()
        {
            IncomeProducts.Clear();
            IsSubmitting = IsCanceled = IsSubmitted = false;
        }
    }

}
