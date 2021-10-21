using System;
using System.Windows.Input;

namespace ZadanieRekrutacyjne.Core
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action<string> action;
        public RelayCommand(Action<string> action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            action(parameter as string);
        }
    }
}
