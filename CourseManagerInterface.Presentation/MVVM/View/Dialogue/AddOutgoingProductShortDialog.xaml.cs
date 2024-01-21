using CourseManagerInterface.Presentation.Interfaces;
using System.Windows;

namespace CourseManagerInterface.Presentation.MVVM.View.Dialogue
{
    /// <summary>
    /// Логика взаимодействия для AddOutgoingProductShortDialog.xaml
    /// </summary>
    public partial class AddOutgoingProductShortDialog : Window, IDialogWindow
    {
        public AddOutgoingProductShortDialog()
        {
            InitializeComponent();
        }

        public void SetDialogResult(bool result)
        {
            DialogResult = result;
        }
    }
}
