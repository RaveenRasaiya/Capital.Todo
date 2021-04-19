using Capital.Application.Common;
using Capital.Core.Interfaces.Common;
using Capital.Core.Interfaces.Handlers;
using System;
using System.Threading.Tasks;

namespace Capital.Application.Services
{
    public sealed class QueryDispatchService : IQueryDispatchService
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryDispatchService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResult> DispatchAsync<TResult>(IQuery<TResult> query)
        {
            Type type = typeof(IQueryHandler<,>);

            Type[] typeArgs = { query.GetType(), typeof(TResult) };

            Type handlerType = type.MakeGenericType(typeArgs);

            dynamic handler = _serviceProvider.GetService(handlerType);

            TResult result = await handler.HandleAsync((dynamic)query);

            return result;
        }
    }
}
