using System;
using System.Windows.Input;

namespace ZadanieRekrutacyjne.Core
{
    public class UpdateViewCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private MainWindowPageViewModel viewModel;
        public UpdateViewCommand(MainWindowPageViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter.ToString() == "AllKeys")
            {
                viewModel.SelectedViewModel = new KeysListPageViewModel();
            }
            else if (parameter.ToString() == "AllEmployees")
            {
                viewModel.SelectedViewModel = new EmployeesListPageViewModel();
            }
            else if (parameter.ToString() == "TakenKeys")
            {
                viewModel.SelectedViewModel = new TakenKeyCheckerPageViewModel();
            }
            else if (parameter.ToString() == "TakeKey")
            {

            }
        }
    }
}
