using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BlueOrigin.Wpf.Controls.Core.Commands
{
    internal class ActionCommand : ICommand
    {
        private Action _execute;
        private Func<bool> _canExecute;

        public ActionCommand(Action execute)
        {
            _execute = execute;
        }

        public ActionCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute.Invoke();
        }

        public void Execute(object parameter)
        {
            _execute?.Invoke();
        }
    }
}
