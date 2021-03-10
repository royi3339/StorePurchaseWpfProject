using System;
using System.Windows.Input;

namespace PL.Commands
{
    public class genericCommand : ICommand

    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public event Action genericClickEvent;

        public bool CanExecute(object parameter) { return true; }

        public void Execute(object parameter)
        {
            if (genericClickEvent != null)
                genericClickEvent();
        }
    }
}
