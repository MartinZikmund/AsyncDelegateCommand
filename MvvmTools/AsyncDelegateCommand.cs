using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;

namespace MvvmTools
{
    public class AsyncDelegateCommand : AsyncDelegateCommand<object>
    {
        public AsyncDelegateCommand(AsyncExecute execute)
            : base(_ => execute(), _ => true)
        {
        }

        public AsyncDelegateCommand(AsyncExecute execute, Func<bool> canExecute)
            : base(_ => execute(), _ => canExecute())
        {
        }

        public AsyncDelegateCommand(CancellableAsyncExecute execute)
            : base((_, token) => execute(token), _ => true)
        {
        }

        public AsyncDelegateCommand(CancellableAsyncExecute execute, Func<bool> canExecute)
            : base((_, token) => execute(token), _ => canExecute())
        {
        }
    }
}
