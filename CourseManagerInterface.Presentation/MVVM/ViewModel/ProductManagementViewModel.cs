using CourseManagerInterface.Presentation.Core;
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

        public RelayCommand SearchProductsCommand { get; }

        public ProductManagementViewModel()
        {
            SearchProductsCommand = new RelayCommand(CanSearch, SearchProducts); 
        }

        private void SearchProducts(object args)
        {

        }

        private bool CanSearch(object args)
        {
            return !string.IsNullOrEmpty(SearchName) || !string.IsNullOrEmpty(SearchType);
        }
    }

    public class ProductRecord : Core.ViewModel
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
    }
}
