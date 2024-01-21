using CourseManagerInterface.Presentation.Core;
using CourseManagerInterface.Presentation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagerInterface.Presentation.MVVM.ViewModel.Dialogue
{
    public class AddOutgoingProductShortDialogViewModel : Core.ViewModel
    {
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

        public RelayCommand AddProductCommand;

        public AddOutgoingProductShortDialogViewModel()
        {
            AddProductCommand = new RelayCommand(CanBeAdded, Ok);
        }

        public void Ok(object args)
        {
            var window = (IDialogWindow)args;
            window.SetDialogResult(true);
            window.Close();
        }

        public bool CanBeAdded(object args)
        {
            return int.TryParse(Id, out int result);
        }
    }
}
