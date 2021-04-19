using Capital.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Capital.Infrastructure.Context
{
    public class ToDoDbContext : DbContext
    {
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options)
        { }
        public DbSet<ToDoItem> ToDoItems { get; set; }
    }
}
