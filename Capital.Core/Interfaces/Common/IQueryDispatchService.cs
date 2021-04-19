using Capital.Core.Interfaces.Queries;
using System.Threading.Tasks;

namespace Capital.Core.Interfaces.Common
{
    public interface IQueryDispatchService
    {
        Task<T> DispatchAsync<T>(IQuery<T> query);
    }
}
