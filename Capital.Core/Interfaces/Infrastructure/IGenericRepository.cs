using Capital.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Capital.Core.Interfaces.Infrastructure
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<int> Add(T entity);
        Task<bool> Update(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int entityId);
        Task<bool> Remove(T entity);
    }
}
