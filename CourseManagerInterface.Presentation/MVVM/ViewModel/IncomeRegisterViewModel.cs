using AutoMapper;
using CourseManagerInterface.Presentation.Commands.List;
using CourseManagerInterface.Presentation.Core;
using CourseManagerInterface.Presentation.Dialogues;
using CourseManagerInterface.Presentation.MVVM.View.Dialogue;
using CourseManagerInterface.Presentation.MVVM.ViewModel.Additional;
using CourseManagerInterface.Presentation.MVVM.ViewModel.Dialogue;
using CourseManagerInterface.Presentation.Requests;
using CourseManagerInterface.Presentation.Requests.List;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CourseManagerInterface.Presentation.MVVM.ViewModel
{
    public class IncomeRegisterViewModel : Core.ViewModel
    {
        private ObservableCollection<IncomeProductRecord> _incomeProducts = new ObservableCollection<IncomeProductRecord>();
        public ObservableCollection<IncomeProductRecord> IncomeProducts
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

        private readonly DialogueService _dialogueService;
        private readonly RequestsService _requestsService;
        private readonly IMapper _mapper;
        private readonly AddIncomeProductFoundCommandCallbackService _productFoundCallbackService;

        public RelayCommand ShowAddProductDialogCommand { get; }
        public RelayCommand SendCreationRequestCommand { get; }

        public IncomeRegisterViewModel(DialogueService dialogueService, RequestsService requestsService, IMapper mapper, AddIncomeProductFoundCommandCallbackService productFoundCallbackService)
        {
            _dialogueService = dialogueService;

            ShowAddProductDialogCommand = new RelayCommand(args => true, ShowDialogAction);
            SendCreationRequestCommand = new RelayCommand(args => IncomeProducts.Any(), Create);

            _requestsService = requestsService;
            _mapper = mapper;
            _productFoundCallbackService = productFoundCallbackService;
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
                int id = int.Parse(dialogueViewModel.Id);
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
            IncomeProductRecord newProductRecord = new IncomeProductRecord()
            {
                Id = id,
                Name = name,
                Description = description ?? string.Empty,
                Price = price,
                Count = count,
                Type = type
            };

            IncomeProducts.Add(newProductRecord);
        }

        private void Create(object args)
        {
            var products = _mapper.Map<List<RequestIncomeProduct>>(IncomeProducts);

            AddIncomeRequestArguments requestArguments = new AddIncomeRequestArguments()
            {
                DateTime = DateTime.Now,
                Supplier = Supplier,
                Products = products
            };

            _requestsService.MakeRequest<AddIncomeRequest>(requestArguments);
        }
    }
    
}
