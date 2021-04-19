using Capital.Application.Common;
using System.Threading.Tasks;

namespace Capital.Core.Interfaces.Handlers
{
    public interface ICommandHandler<TCommand, TOutput> where TCommand : ICommand<TOutput>
    {
        Task<TOutput> HandleAsync(TCommand command);
    }
}