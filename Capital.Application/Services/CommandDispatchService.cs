using Capital.Application.Common;
using Capital.Core.Interfaces.Common;
using Capital.Core.Interfaces.Handlers;
using System;
using System.Threading.Tasks;

namespace Capital.Application.Services
{
    public sealed class CommandDispatchService : ICommandDispatchService
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatchService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResult> DispatchAsync<TResult>(ICommand<TResult> command)
        {
            var type = typeof(ICommandHandler<,>);

            var argTypes = new Type[] { command.GetType(), typeof(TResult) };

            var handlerType = type.MakeGenericType(argTypes);

            dynamic handler = _serviceProvider.GetService(handlerType);

            TResult result = await handler.HandleAsync((dynamic)command);

            return result;
        }
    }
}