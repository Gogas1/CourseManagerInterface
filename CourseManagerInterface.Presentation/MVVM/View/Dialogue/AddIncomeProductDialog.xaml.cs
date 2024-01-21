using CourseManagerInterface.Presentation.Interfaces;
using System.Windows;

namespace CourseManagerInterface.Presentation.MVVM.View.Dialogue
{
    /// <summary>
    /// Логика взаимодействия для AddIncomeProductDialog.xaml
    /// </summary>
    public partial class AddIncomeProductDialog : Window, IDialogWindow
    {
        public AddIncomeProductDialog()
        {
            InitializeComponent();
        }

        public void SetDialogResult(bool result)
        {
            DialogResult = result;
        }
    }
}
