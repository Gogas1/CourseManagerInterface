using CourseManagerInterface.Presentation.Core;
using CourseManagerInterface.Presentation.Models;
using CourseManagerInterface.Presentation.Requests;
using CourseManagerInterface.Presentation.Requests.List;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CourseManagerInterface.Presentation.Requests.List.SearchOutgoingsRequest;

namespace CourseManagerInterface.Presentation.MVVM.ViewModel
{
    public class OutgoingManagementViewModel : Core.ViewModel
    {
        private AsyncObservableCollection<OutgoingRecord> _outgoings = new();
        public AsyncObservableCollection<OutgoingRecord> Outgoings
        {
            get => _outgoings;
            set
            {
                _outgoings = value;
                OnPropertyChanged(nameof(Outgoings));
            }
        }

        private OutgoingRecord? _selectedOutgoing;
        public OutgoingRecord? SelectedOutgoing
        {
            get => _selectedOutgoing;
            set
            {
                _selectedOutgoing = value;
                OnPropertyChanged(nameof(SelectedOutgoing));
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

        public RelayCommand SearchOutgoingsCommand { get; }

        private readonly RequestsService _requestsService;
        public OutgoingManagementViewModel(RequestsService requestsService)
        {
            StartDate = DateTime.Now.AddDays(-1).Date;
            EndDate = DateTime.Now.AddDays(1).Date.AddMicroseconds(-1);

            _requestsService = requestsService;

            SearchOutgoingsCommand = new RelayCommand(args => StartDate < EndDate, SearchOutgoings);
        }

        public void ShowSearchResult(IEnumerable<Outgoing> outgoingsFound)
        {            
            Outgoings.Clear();
            foreach (var outgoing in outgoingsFound)
            {
                OutgoingRecord newIncomeRecord = new OutgoingRecord
                {
                    Id = outgoing.Id,
                    CreatedAt = outgoing.CreatedAt,
                    Manager = outgoing.Manager,
                    Summ = outgoing.Summ
                };
                Outgoings.Add(newIncomeRecord);
            }
            IsSearching = false;
        }

        private void SearchOutgoings(object args)
        {
            SearchOutgoingsRequestArguments requestArguments = new SearchOutgoingsRequestArguments
            {
                StartDate = this.StartDate, 
                EndDate = this.EndDate,
            };

            IsSearching = true;
            _requestsService.MakeRequest<SearchOutgoingsRequest>(requestArguments);
        }
    }

    public class OutgoingRecord : Core.ViewModel
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
