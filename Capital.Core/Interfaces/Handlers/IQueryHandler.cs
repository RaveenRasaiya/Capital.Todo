using Capital.Core.Interfaces.Queries;
using System.Threading.Tasks;

namespace Capital.Core.Interfaces.Handlers
{
    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}