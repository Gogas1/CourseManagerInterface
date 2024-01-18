using CourseManagerInterface.Presentation.Core;
using CourseManagerInterface.Presentation.Requests;
using CourseManagerInterface.Presentation.Requests.List;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagerInterface.Presentation.MVVM.ViewModel
{
    public class TestViewModel : Core.ViewModel
    {
        #region ViewProperties

        private string _someString = string.Empty;
        public string SomeString 
        { 
            get => _someString;
            set
            {
                _someString = value;
                OnPropertyChanged(nameof(SomeString));
            } 
        }

        private string _someInt = string.Empty;
        public string SomeInt
        {
            get => _someInt;
            set
            {
                _someInt = value;
                OnPropertyChanged(nameof(SomeInt));
            }
        }

        private DateTime _requestedDateTime;
        public DateTime RequestedDateTime
        {
            get => _requestedDateTime;
            set
            {
                _requestedDateTime = value;
                OnPropertyChanged(nameof(RequestedDateTime));
            }
        }

        #endregion ViewProperties

        #region Commands

        public RelayCommand SendMessageCommand { get; set; }

        #endregion Commands

        #region Fields

        private readonly RequestsService _requestsService;

        #endregion

        #region Constructors

        public TestViewModel(RequestsService requestsService)
        {
            _requestsService = requestsService;

            SendMessageCommand = new RelayCommand(CanBeExecuted, SendTestRequest);
        }

        #endregion Constructors

        private bool CanBeExecuted(object args)
        {
            if (int.TryParse(SomeInt, out int result))
            {
                return !string.IsNullOrEmpty(SomeString);
            }

            return false;
        }

        public void SendTestRequest(object args)
        {
            TestRequestArguments testRequestArguments = new TestRequestArguments()
            {
                SomeInt = int.Parse(SomeInt),
                SomeString = SomeString
            };

            _requestsService.MakeRequest<TestRequest>(testRequestArguments);
        }

        public void SetTime(DateTime dateTime)
        {
            RequestedDateTime = dateTime;
        }
    }
}
