using System.Windows.Controls;
using ZadanieRekrutacyjne.Core;

namespace ZadanieRekrutacyjne
{
    /// <summary>
    /// Interaction logic for KeysListPage.xaml
    /// </summary>
    public partial class KeysListPage : Page
    {
        public KeysListPage()
        {
            InitializeComponent();

            DataContext = new KeysListPageViewModel();
        }
    }
}
