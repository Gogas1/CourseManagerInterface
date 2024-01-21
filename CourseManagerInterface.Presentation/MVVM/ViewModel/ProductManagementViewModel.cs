using CourseManagerInterface.Presentation.Core;
using CourseManagerInterface.Presentation.Models;
using CourseManagerInterface.Presentation.MVVM.ViewModel.Additional;
using CourseManagerInterface.Presentation.Requests;
using CourseManagerInterface.Presentation.Requests.List;

namespace CourseManagerInterface.Presentation.MVVM.ViewModel
{
    public class ProductManagementViewModel : Core.ViewModel
    {
        private ProductRecord _selectedProduct;
        public ProductRecord SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
                TypeFeature = value?.Type ?? string.Empty;
                PriceFeature = value?.Price.ToString() ?? string.Empty;
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

        private string _searchName = string.Empty;
        public string SearchName
        {
            get => _searchName;
            set
            {
                _searchName = value;
                OnPropertyChanged(nameof(SearchName));
            }
        }

        private string _searchType = string.Empty;
        public string SearchType
        {
            get => _searchType;
            set
            {
                _searchType = value;
                OnPropertyChanged(nameof(SearchType));
            }
        }

        private string _typeFeature = string.Empty;
        public string TypeFeature
        {
            get => _typeFeature;
            set
            {
                _typeFeature = value;
                OnPropertyChanged(nameof(TypeFeature));
            }
        }

        private string _priceFeature;
        public string PriceFeature
        {
            get => _priceFeature;
            set
            {
                _priceFeature = value;
                OnPropertyChanged(nameof(PriceFeature));
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

        public RelayCommand SearchProductsCommand { get; }
        public RelayCommand GetAllProductsCommand { get; }
        public RelayCommand SaveProductFeaturesCommand { get; }

        private readonly RequestsService _requestsService;

        public ProductManagementViewModel(RequestsService requestsService)
        {
            SearchProductsCommand = new RelayCommand(CanSearch, SearchProducts);
            GetAllProductsCommand = new RelayCommand(args => true, SearchProducts);
            SaveProductFeaturesCommand = new RelayCommand(CanUpdate, SaveProductFeatures);
            _requestsService = requestsService;
        }

        public void ShowFoundProducts(IEnumerable<Product> foundProducts)
        {
            foreach (var product in foundProducts)
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
            IsSearching = false;
        }

        private void SearchProducts(object args)
        {
            IsSearching = true;
            Products.Clear();
            SearchProductsRequestArguments arguments = new SearchProductsRequestArguments()
            {
                Name = SearchName,
                Type = SearchType
            };

            _requestsService.MakeRequest<SearchProductsRequest>(arguments);
        }

        private void SaveProductFeatures(object args)
        {
            SaveProductFeatureRequestArgs arguments = new SaveProductFeatureRequestArgs
            {
                Id = SelectedProduct.Id,
                Type = TypeFeature,
                Price = decimal.Parse(PriceFeature)
            };

            _requestsService.MakeRequest<SaveProductFeaturesRequest>(arguments);
        }

        private bool CanUpdate(object args)
        {
            return decimal.TryParse(PriceFeature, out decimal _) && !string.IsNullOrEmpty(TypeFeature);
        }

        private bool CanSearch(object args)
        {
            return !string.IsNullOrEmpty(SearchName) || !string.IsNullOrEmpty(SearchType);
        }
    }
}
