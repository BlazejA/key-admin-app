using System.Windows;
using ZadanieRekrutacyjne.Core;

namespace ZadanieRekrutacyjne
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowPageViewModel();
        }
    }
}
