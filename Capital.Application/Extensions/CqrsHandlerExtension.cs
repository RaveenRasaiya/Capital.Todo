using Capital.Application.Common;
using Capital.Core.Interfaces.Handlers;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Capital.Application.Extensions
{
    public static class CqrsHandlerExtension
    {
        public static void AddCqrsHandlers(this IServiceCollection services)
        {
            var commandHandlers = typeof(ICommand<>).Assembly.GetTypes()
             .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<,>)));

            foreach (var handler in commandHandlers)
            {
                services.AddScoped(handler.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<,>)), handler);
            }

            var queryHanlders = typeof(IQuery<>).Assembly.GetTypes()
             .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>)));

            foreach (var handler in queryHanlders)
            {
                services.AddScoped(handler.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>)), handler);
            }
        }
    }
}
