using CourseManagerInterface.Presentation.Commands.List;
using CourseManagerInterface.Presentation.Core;
using CourseManagerInterface.Presentation.Interfaces;
using CourseManagerInterface.Presentation.Models;
using CourseManagerInterface.Presentation.MVVM.ViewModel.Additional;
using CourseManagerInterface.Presentation.Requests;
using CourseManagerInterface.Presentation.Requests.List;

namespace CourseManagerInterface.Presentation.MVVM.ViewModel.Dialogue
{
    public class AddOutgoingProductShortDialogViewModel : Core.ViewModel
    {
        private ProductRecord _selectedProduct;
        public ProductRecord SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
                SelectProduct(value);
            }
        }

        private ProductRecord _finalProduct = new ProductRecord();
        public ProductRecord FinalProduct
        {
            get => _finalProduct;
            set
            {
                _finalProduct = value;
                OnPropertyChanged(nameof(FinalProduct));
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

        #region FormFields

        private string _idSearch = string.Empty;
        public string IdSearch
        {
            get => _idSearch;
            set
            {
                _idSearch = value;
                OnPropertyChanged(nameof(IdSearch));
            }
        }

        private string _nameSearch = string.Empty;
        public string NameSearch
        {
            get => _nameSearch;
            set
            {
                _nameSearch = value;
                OnPropertyChanged(nameof(NameSearch));
            }
        }

        #endregion FormFields

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

        public RelayCommand AddProductCommand { get; }
        public RelayCommand SearchProductsCommand { get; }

        private readonly RequestsService _requestService;
        private readonly OutgoingProductsFoundCommandCallbackService _outgoingProductsFoundCallbackService;

        public AddOutgoingProductShortDialogViewModel(RequestsService requestService, OutgoingProductsFoundCommandCallbackService outgoingProductsFoundCallbackService)
        {
            AddProductCommand = new RelayCommand(CanBeAdded, Ok);
            SearchProductsCommand = new RelayCommand(CanSearch, SearchProducts);

            _requestService = requestService;
            _outgoingProductsFoundCallbackService = outgoingProductsFoundCallbackService;
            _outgoingProductsFoundCallbackService.OnExecuting += ShowProducts;
        }

        public void UnsubscribeEvents()
        {
            _outgoingProductsFoundCallbackService.OnExecuting -= ShowProducts;
        }

        public void Ok(object args)
        {
            var window = (IDialogWindow)args;
            window.SetDialogResult(true);
            window.Close();
        }

        public bool CanBeAdded(object args)
        {
            return SelectedProduct != null && FinalProduct.Amount > 0;
        }
        public bool CanSearch(object args)
        {
            return int.TryParse(IdSearch, out int _) || !string.IsNullOrEmpty(NameSearch);
        }

        private void SearchProducts(object args)
        {
            IsSearching = true;
            Products.Clear();

            int id;
            int.TryParse(IdSearch, out id);
            SearchProductsOutgoingRequestArguments requestArguments = new SearchProductsOutgoingRequestArguments
            {
                Id = id,
                Name = NameSearch
            };

            _requestService.MakeRequest<SearchProductsOutgoingRequest>(requestArguments);
        }

        private void ShowProducts(IEnumerable<Product> products)
        {
            foreach (var product in products)
            {
                ProductRecord productRecord = new ProductRecord
                {
                    Id = product.Id,
                    Description = product.Description,
                    Amount = product.Amount,
                    Name = product.Name,
                    Price = product.Price,
                    Type = product.Type,
                };
                Products.Add(productRecord);
            }
            IsSearching = false;
        }

        private void SelectProduct(ProductRecord product)
        {
            FinalProduct = new ProductRecord
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Amount = 0,
                Price = product.Price,
                Type = product.Type,
            };
        }
    }
}
