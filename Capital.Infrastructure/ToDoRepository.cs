using Capital.Core.Interfaces.Infrastructure;
using Capital.Core.Models;
using Capital.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<int> Add(ToDoItem entity)
        {
            _toDoDbContext.ToDoItems.Add(entity);
            return await _toDoDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ToDoItem>> GetAll()
        {
            return await _toDoDbContext.ToDoItems.ToListAsync();
        }

        public async Task<ToDoItem> GetById(int entityId)
        {
            return await _toDoDbContext.ToDoItems.FirstOrDefaultAsync(x => x.Id == entityId);

        }

        public Task<bool> Remove(ToDoItem entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(ToDoItem entity)
        {
            throw new NotImplementedException();
        }
    }
}
