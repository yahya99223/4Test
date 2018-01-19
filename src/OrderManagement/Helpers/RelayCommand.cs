using System;
using System.Windows.Input;

namespace OrderManagement
{
    public class RelayCommand : ICommand
    {
        private Action<object> commandAction;
        private Func<bool> canExecuteFunc;
        private Func<object, bool> canExecuteFuncWithParameter;
        private System.Threading.SynchronizationContext syncContext;

        public RelayCommand(Action<object> action, Func<object, bool> canExecute)
            : this(action)
        {
            canExecuteFuncWithParameter = canExecute;
        }

        public RelayCommand(Action<object> action, Func<bool> canExecute)
            : this(action)
        {
            canExecuteFunc = canExecute;
        }

        public RelayCommand(Action<object> action)
        {
            commandAction = action;
            syncContext = System.Threading.SynchronizationContext.Current;
        }

        public bool CanExecute(object parameter)
        {
            if (canExecuteFunc != null)
                return canExecuteFunc();
            else
                if (canExecuteFuncWithParameter != null)
                    return canExecuteFuncWithParameter(parameter);
                else
                    return true;
        }

        public void NotifyCanExecuteChanged()
        {
            if ((canExecuteFunc != null || canExecuteFuncWithParameter != null) && CanExecuteChanged != null)
            {
                syncContext.Post(new System.Threading.SendOrPostCallback((o) =>
                {
                    CanExecuteChanged(o, EventArgs.Empty);
                }), this);
            }
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            commandAction(parameter);
        }
    }
}
