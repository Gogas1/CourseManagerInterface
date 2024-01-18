using CourseManagerInterface.Presentation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagerInterface.Presentation.MVVM.ViewModel
{
    public class IncomeManagementViewModel : Core.ViewModel
    {
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

        private DateTime _dateTime;
        public DateTime DateTime
        {
            get => _dateTime;
            set
            {
                _dateTime = value;
                OnPropertyChanged(nameof(DateTime));
            }
        }

        private string _supplier;
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
    }
}
