using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ZadanieRekrutacyjne.Core
{
    public class MainWindowPageViewModel : BaseViewModel
    {
        private BaseViewModel _selectedViewModel;
        public BaseViewModel SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        public ICommand UpdateViewCommand { get; set; }

        public MainWindowPageViewModel()
        {
            UpdateViewCommand = new UpdateViewCommand(this);
        }
    }
}
