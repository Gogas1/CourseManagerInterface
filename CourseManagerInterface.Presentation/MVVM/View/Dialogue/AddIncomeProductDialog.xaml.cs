using CourseManagerInterface.Presentation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
