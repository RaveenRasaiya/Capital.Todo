using Capital.Application.Models;
using Capital.Core.Entities;
using Capital.Core.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Capital.Application.Mappers
{
    public static class ToDoItemMapper
    {
        public static ToDoItem ToTableModel(this ToDoModel toDoModel)
        {
            return new ToDoItem
            {
                Description = toDoModel.Description,
                Id = toDoModel.Id,
                Title = toDoModel.Title,
                Status = (ToDoItemStatus)toDoModel.StatusId
            };
        }
        public static IEnumerable<ToDoModel> ToViewModel(this IEnumerable<ToDoItem> toDoItems)
        {
            return toDoItems.Select(x => x.ToViewModel()).ToList();
        }
        public static ToDoModel ToViewModel(this ToDoItem toDoItem)
        {
            return new ToDoModel
            {
                Description = toDoItem.Description,
                Id = toDoItem.Id,
                Title = toDoItem.Title,
                StatusId = (int)toDoItem.Status,
                Status = toDoItem.Status.ToString()
            };
        }
    }
}
