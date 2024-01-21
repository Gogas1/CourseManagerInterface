using CourseManagerInterface.Presentation.Commands.List;
using CourseManagerInterface.Presentation.Core;
using CourseManagerInterface.Presentation.Dialogues;
using CourseManagerInterface.Presentation.Models;
using CourseManagerInterface.Presentation.MVVM.View.Dialogue;
using CourseManagerInterface.Presentation.MVVM.ViewModel.Additional;
using CourseManagerInterface.Presentation.MVVM.ViewModel.Dialogue;
using CourseManagerInterface.Presentation.Requests;
using CourseManagerInterface.Presentation.Requests.List;

namespace CourseManagerInterface.Presentation.MVVM.ViewModel
{
    public class CreateOutgoingViewModel : Core.ViewModel
    {
        private ProductRecord _selectedProduct;
        public ProductRecord SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
            }
        }

        private AsyncObservableCollection<ProductRecord> _products = new();
        public AsyncObservableCollection<ProductRecord> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        private string _manager = string.Empty;
        public string Manager
        {
            get => _manager;
            set
            {
                _manager = value;
                OnPropertyChanged(nameof(Manager));
            }
        }

        private string _summ;
        public string Summ
        {
            get => _summ;
            set
            {
                _summ = value;
                OnPropertyChanged(nameof(Summ));
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

        public RelayCommand ShowDialogAddProductFastCommand { get; }
        public RelayCommand SubmitOutgoingCommand { get; }
        public RelayCommand RemoveSelectedProductCommand { get; }
        public RelayCommand ResetCommand { get; }

        private readonly DialogueService _dialogueService;
        private readonly RequestsService _requestsService;
        private readonly OutgoingProductsFoundCommandCallbackService _callbackService;

        private decimal summ;

        public CreateOutgoingViewModel(DialogueService dialogueService, RequestsService requestsService, OutgoingProductsFoundCommandCallbackService callbackService)
        {
            _dialogueService = dialogueService;
            _requestsService = requestsService;

            ShowDialogAddProductFastCommand = new RelayCommand(args => !IsSubmitting, ShowDialogAddProductFastAction);
            SubmitOutgoingCommand = new RelayCommand(CanSubmit, SendSubmitRequest);
            RemoveSelectedProductCommand = new RelayCommand(args => SelectedProduct != null && !IsSubmitting, RemoveSelectedProduct);
            ResetCommand = new RelayCommand(args => true, Reset);
            _callbackService = callbackService;
        }

        public void AddProduct(bool success, Product? product)
        {
            if (success && product != null)
            {
                ProductRecord productRecord = new ProductRecord
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Amount = product.Amount,
                    Price = product.Price,
                    Type = product.Type
                };

                Products.Add(productRecord);
            }
        }

        private void ShowDialogAddProductFastAction(object args)
        {
            AddProductFast(viewModel => _dialogueService.ShowDialog<AddOutgoingProductShortDialog>(viewModel));
        }
        private void AddProductFast(Func<AddOutgoingProductShortDialogViewModel, bool> showDialog)
        {
            var dialogueViewModel = new AddOutgoingProductShortDialogViewModel(_requestsService, _callbackService);

            bool success = showDialog(dialogueViewModel);
            dialogueViewModel.UnsubscribeEvents();
            if (success)
            {
                var addedProduct = dialogueViewModel.FinalProduct;
                Products.Add(addedProduct);
            }

            UpdateSummLabel();
        }

        private void UpdateSummLabel()
        {
            summ = Products.Sum(p => p.Price * (decimal)p.Amount);
            Summ = $"Summ: {summ}";
        }

        private void SendSubmitRequest(object args)
        {
            IsSubmitting = true;
            SubmitOutgoingRequestArguments requestArguments = new();
            foreach (var item in Products)
            {
                var product = new Product
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Amount = item.Amount,
                    Price = item.Price,
                    Type = item.Type
                };
                requestArguments.Products.Add(product);
            }
            requestArguments.Manager = Manager;

            _requestsService.MakeRequest<SubmitOutgoingRequest>(requestArguments);
        }

        private void RemoveSelectedProduct(object args)
        {
            Products.Remove(SelectedProduct);
            UpdateSummLabel();
        }

        private bool CanSubmit(object args)
        {
            return Products.Any() && !string.IsNullOrEmpty(Manager) && !IsSubmitting;
        }

        public void SetSubmitResult(bool submitResult)
        {
            IsSubmitting = false;
            IsSubmitted = submitResult;
            IsCanceled = !submitResult;
        }

        private void Reset(object args)
        {
            Products.Clear();
            UpdateSummLabel();
            IsSubmitting = IsCanceled = IsSubmitted = false;
        }
    }
}
