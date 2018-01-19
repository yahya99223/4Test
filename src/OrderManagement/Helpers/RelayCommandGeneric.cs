using System;
using System.Diagnostics;
using System.Windows.Input;

namespace OrderManagement
{
    /// <summary>
    ///     A command whose sole purpose is to relay its functionality to other objects by invoking delegates. The default
    ///     return value for the CanExecute method is 'true'.
    /// </summary>
    public class RelayCommand<T> : ICommand
    {
        #region Declarations

        private readonly Predicate<T> canExecute;
        private readonly Func<Boolean> canExecuteWithoutParameter;
        private readonly Action<T> execute;

        #endregion


        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="RelayCommand&lt;T&gt;" /> class and the command can always be
        ///     executed.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public RelayCommand(Action<T> execute)
        {
            this.execute = execute;
            canExecute = null;
        }


        /// <summary>
        ///     Initializes a new instance of the <see cref="RelayCommand&lt;T&gt;" /> class.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            this.execute = execute;
            this.canExecute = canExecute;
        }


        public RelayCommand(Action<T> execute, Func<Boolean> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            this.execute = execute;
            canExecuteWithoutParameter = canExecute;
        }

        #endregion


        #region ICommand Members

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (canExecute != null)
                    CommandManager.RequerySuggested += value;

                if (canExecuteWithoutParameter != null)
                    CommandManager.RequerySuggested += value;
            }
            remove
            {
                if (canExecute != null)
                    CommandManager.RequerySuggested -= value;

                if (canExecuteWithoutParameter != null)
                    CommandManager.RequerySuggested -= value;
            }
        }


        [DebuggerStepThrough]
        public Boolean CanExecute(Object parameter)
        {
            var result = canExecute == null || canExecute((T) parameter);
            if (!result)
                return canExecuteWithoutParameter == null || canExecuteWithoutParameter();
            return true;
        }


        public void Execute(Object parameter)
        {
            execute((T) parameter);
        }

        #endregion
    }
}