using CourseManagerInterface.Presentation.Core;
using CourseManagerInterface.Presentation.Models;
using CourseManagerInterface.Presentation.Requests;
using CourseManagerInterface.Presentation.Requests.List;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagerInterface.Presentation.MVVM.ViewModel
{
    public class ProductManagementViewModel : Core.ViewModel
    {
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

        private readonly RequestsService _requestsService;

        public ProductManagementViewModel(RequestsService requestsService)
        {
            SearchProductsCommand = new RelayCommand(CanSearch, SearchProducts);
            GetAllProductsCommand = new RelayCommand(args => true, SearchProducts);
            _requestsService = requestsService;
        }

        public void ShowFoundProducts(IEnumerable<Product> foundProducts)
        {
            foreach (var item in foundProducts)
            {
                ProductRecord productRecord = new ProductRecord(item);
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

        private bool CanSearch(object args)
        {
            return !string.IsNullOrEmpty(SearchName) || !string.IsNullOrEmpty(SearchType);
        }
    }

    public class ProductRecord : Core.ViewModel
    {
        public ProductRecord(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            Amount = product.Amount;
            Price = product.Price;
            Type = product.Type;
        }

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

        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _description = string.Empty;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public double _amount;
        public double Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }

        private string _type = string.Empty;
        public string Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged(nameof(Type));
            }
        }

        private decimal _price;
        public decimal Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }
    }
}
