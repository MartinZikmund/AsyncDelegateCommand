using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MvvmTools
{
    public class AsyncDelegateCommand<T> : ICommand
    {
        private readonly CancellableAsyncExecute<T> _execute;
        private readonly Func<T, bool> _canExecute;
        private bool _isExecuting;
        private CancellationTokenSource _cancellationTokenSource;

        public AsyncDelegateCommand(AsyncExecute<T> execute)
            : this(execute, _ => true)
        {
        }

        public AsyncDelegateCommand(AsyncExecute<T> execute, Func<T, bool> canExecute)
            : this((input, _) => execute(input), canExecute)
        {
        }

        public AsyncDelegateCommand(CancellableAsyncExecute<T> execute)
            : this(execute, _ => true)
        {
        }

        public AsyncDelegateCommand(CancellableAsyncExecute<T> execute, Func<T, bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }

        public event EventHandler CanExecuteChanged;

        public async void Execute(object parameter)
        {            
            try
            {
                _cancellationTokenSource = new CancellationTokenSource();
                _isExecuting = true;
                RaiseCanExecuteChanged();
                await _execute((T)parameter, _cancellationTokenSource.Token);
            }
            finally
            {
                _isExecuting = false;
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;
                RaiseCanExecuteChanged();
            }
        }

        public void Cancel()
        {
            if (_isExecuting)
            {
                _cancellationTokenSource.Cancel();
            }
        }

        public bool CanExecute(object parameter)
        {
            return !_isExecuting                   
                   && _canExecute((T)parameter);
        }

        public void RaiseCanExecuteChanged() =>
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
