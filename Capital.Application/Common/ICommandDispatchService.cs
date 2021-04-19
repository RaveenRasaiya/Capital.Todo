using Capital.Application.Common;
using System.Threading.Tasks;

namespace Capital.Core.Interfaces.Common
{
    public interface ICommandDispatchService
    {
        Task<T> DispatchAsync<T>(ICommand<T> command);
    }
}
