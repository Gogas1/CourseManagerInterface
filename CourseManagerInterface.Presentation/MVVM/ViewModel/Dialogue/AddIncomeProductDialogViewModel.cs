using CourseManagerInterface.Presentation.Commands.List;
using CourseManagerInterface.Presentation.Core;
using CourseManagerInterface.Presentation.Interfaces;
using CourseManagerInterface.Presentation.Requests;
using CourseManagerInterface.Presentation.Requests.List;

namespace CourseManagerInterface.Presentation.MVVM.ViewModel.Dialogue
{
    public class AddIncomeProductDialogViewModel : Core.ViewModel
    {
        #region Props

        private string _id = string.Empty;
        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        //String
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

        //String
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

        //Double
        private string _count = string.Empty;
        public string Count
        {
            get => _count;
            set
            {
                _count = value;
                OnPropertyChanged(nameof(Count));
            }
        }

        //Decimal
        private string _price = string.Empty;
        public string Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        //String
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

        private bool _showSearchResult;
        public bool ShowSearchResult
        {
            get => _showSearchResult;
            set
            {
                _showSearchResult = value;
                OnPropertyChanged(nameof(ShowSearchResult));
            }
        }

        private bool _searchingFailture;
        public bool SearchingFailture
        {
            get => _searchingFailture;
            set
            {
                _searchingFailture = value;
                OnPropertyChanged(nameof(SearchingFailture));
            }
        }

        private AsyncObservableCollection<ComboboxProductItem> _foundProducts = new();
        public AsyncObservableCollection<ComboboxProductItem> FoundProducts
        {
            get => _foundProducts;
            set
            {
                _foundProducts = value;
                OnPropertyChanged(nameof(FoundProducts));
            }
        }

        private ComboboxProductItem? _selectedProduct;
        public ComboboxProductItem? SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
                ProductSelected();
            }
        }

        #endregion Props

        public RelayCommand SubmitCommand { get; }
        public RelayCommand SearchCommand { get; }

        private readonly AddIncomeProductFoundCommandCallbackService _productFoundCallbackService;
        private readonly RequestsService _requestsService;

        public AddIncomeProductDialogViewModel(AddIncomeProductFoundCommandCallbackService productFoundCallbackService, RequestsService requestsService)
        {
            SubmitCommand = new RelayCommand(CanBeAdded, Ok);
            SearchCommand = new RelayCommand(CanSearch, Search);

            _productFoundCallbackService = productFoundCallbackService;
            _productFoundCallbackService.OnExecuting += ProductFound;
            _requestsService = requestsService;
        }

        public void UnsubscribeEvents()
        {
            _productFoundCallbackService.OnExecuting -= ProductFound;
        }

        private void Search(object args)
        {
            int id;
            int.TryParse(Id, out id);

            SearchProductRequestArguments arguments = new SearchProductRequestArguments()
            {
                Id = id,
                Name = Name
            };

            FoundProducts.Clear();
            SearchingFailture = false;
            IsSearching = true;
            ShowSearchResult = false;

            _requestsService.MakeRequest<AddIncomeSearchProductRequest>(arguments);
        }

        private void ProductFound(AddIncomeProductFoundCommandData commandData)
        {
            IsSearching = false;

            if (commandData.ProductFound)
            {
                ShowSearchResult = true;
                foreach (var product in commandData.FoundProducts)
                {
                    var foundProduct = new ComboboxProductItem()
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Type = product.Type,
                        Description = product.Description,
                    };
                    FoundProducts.Add(foundProduct);
                }
            }
            else
            {
                SearchingFailture = true;
            }
        }

        private void ProductSelected()
        {
            if (SelectedProduct != null)
            {
                Id = SelectedProduct.Id.ToString();
                Name = SelectedProduct.Name;
                Description = SelectedProduct.Description;
                Type = SelectedProduct.Type;
            }
        }

        private void Ok(object args)
        {
            var window = (IDialogWindow)args;
            window.SetDialogResult(true);
            window.Close();
        }

        #region CommandsCondiditons

        private bool CanBeAdded(object args)
        {
            try
            {
                if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Type))
                {
                    double.Parse(Count);
                    decimal.Parse(Price);

                    return true;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }

        private bool CanSearch(object args)
        {
            return int.TryParse(Id, out int _) || !string.IsNullOrEmpty(Name);
        }

        #endregion CommandsCondiditons
    }

    public class ComboboxProductItem : Core.ViewModel
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
    }
}
