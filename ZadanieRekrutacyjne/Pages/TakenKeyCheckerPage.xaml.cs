using System.Windows.Controls;
using ZadanieRekrutacyjne.Core;

namespace ZadanieRekrutacyjne
{
    /// <summary>
    /// Interaction logic for TakenKeyCheckerPage.xaml
    /// </summary>
    public partial class TakenKeyCheckerPage : Page
    {
        public TakenKeyCheckerPage()
        {
            InitializeComponent();

            DataContext = new TakenKeyCheckerPageViewModel();
        }
    }
}
