using System;
using System.Windows.Input;

namespace ZadanieRekrutacyjne.Core
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action<string> mAction;
        public RelayCommand(Action<string> action)
        {
            mAction = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            mAction(parameter as string);
        }
    }
}
