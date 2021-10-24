using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace ZadanieRekrutacyjne
{
    /// <summary>
    /// Interaction logic for EmployeesListPage.xaml
    /// </summary>
    public partial class EmployeesListPage : UserControl
    {
        public EmployeesListPage()
        {
            InitializeComponent();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
