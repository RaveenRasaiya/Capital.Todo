using Capital.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Capital.Core.Interfaces.Infrastructure
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<int> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int entityId);
        Task<bool> RemoveAsync(T entity);
    }
}
