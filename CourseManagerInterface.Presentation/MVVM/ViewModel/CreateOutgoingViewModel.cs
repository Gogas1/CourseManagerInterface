using CourseManagerInterface.Presentation.Core;
using CourseManagerInterface.Presentation.Dialogues;
using CourseManagerInterface.Presentation.Models;
using CourseManagerInterface.Presentation.MVVM.View.Dialogue;
using CourseManagerInterface.Presentation.MVVM.ViewModel.Dialogue;
using CourseManagerInterface.Presentation.Requests;
using CourseManagerInterface.Presentation.Requests.List;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagerInterface.Presentation.MVVM.ViewModel
{
    public class CreateOutgoingViewModel : Core.ViewModel
    {
        private AsyncObservableCollection<Product> _products = new();
        public AsyncObservableCollection<Product> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        public RelayCommand ShowDialogAddProductFastCommand { get; }

        private readonly DialogueService _dialogueService;
        private readonly RequestsService _requestsService;

        public CreateOutgoingViewModel(DialogueService dialogueService, RequestsService requestsService)
        {
            _dialogueService = dialogueService;
            _requestsService = requestsService;
        }

        private void ShowDialogAddProductFastAction(object args)
        {
            AddProductFast(viewModel => _dialogueService.ShowDialog<AddOutgoingProductShortDialog>(viewModel));
        }
        private void AddProductFast(Func<AddOutgoingProductShortDialogViewModel, bool> showDialog)
        {
            var dialogueViewModel = new AddOutgoingProductShortDialogViewModel();

            bool success = showDialog(dialogueViewModel);
            if(success)
            {
                int id = int.Parse(dialogueViewModel.Id);
                _requestsService.MakeRequest<GetOugoingProductFastRequest>(id);
            }
        }
    }
}
