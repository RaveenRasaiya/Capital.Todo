using Capital.Core.Entities;
using Capital.Core.Interfaces.Infrastructure;
using Capital.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Capital.Infrastructure
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly ToDoDbContext _toDoDbContext;

        // as per requirement, it is saved in memory collection.

        public ToDoRepository(ToDoDbContext toDoDbContext)
        {
            _toDoDbContext = toDoDbContext;
        }
        public async Task<int> AddAsync(ToDoItem entity)
        {
            _toDoDbContext.ToDoItems.Add(entity);
            await _toDoDbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<IEnumerable<ToDoItem>> GetAllAsync()
        {
            return await _toDoDbContext.ToDoItems.ToListAsync();
        }

        public async Task<ToDoItem> GetByIdAsync(int entityId)
        {
            return await _toDoDbContext.ToDoItems.FirstOrDefaultAsync(x => x.Id == entityId);
        }

        public async Task<bool> RemoveAsync(ToDoItem entity)
        {
            _toDoDbContext.ToDoItems.Remove(entity);
            return await _toDoDbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(ToDoItem entity)
        {
            _toDoDbContext.ToDoItems.Update(entity);
            return await _toDoDbContext.SaveChangesAsync() > 0;
        }
    }
}
