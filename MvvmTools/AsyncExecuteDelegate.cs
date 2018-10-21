using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MvvmTools
{
    public delegate Task AsyncExecute();
    public delegate Task CancellableAsyncExecute(CancellationToken cancellationToken);
    public delegate Task AsyncExecute<T>(T input);
    public delegate Task CancellableAsyncExecute<T>(T input, CancellationToken cancellationToken);
}
