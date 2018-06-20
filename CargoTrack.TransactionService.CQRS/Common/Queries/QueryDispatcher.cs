using System;
using Ninject;

namespace CargoTrack.TransactionService.CQRS.Common.Queries
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IKernel _kernel;

        public QueryDispatcher(IKernel kernel)
        {
            if (kernel == null) throw new ArgumentNullException(nameof(kernel));

            _kernel = kernel;
        }

        public TResult Execute<TResult>(IQuery<TResult> query)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            var handler = GetQueryHandler(query);

            return handler.Execute((dynamic)query);
        }

        private dynamic GetQueryHandler<TResult>(IQuery<TResult> query)
        {
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));

            return _kernel.Get(handlerType);
        }
    }
}
