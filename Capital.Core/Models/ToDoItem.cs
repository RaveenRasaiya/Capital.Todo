using Capital.Core.Enums;

namespace Capital.Core.Models
{
    public class ToDoItem : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ToDoItemStatus Status { get; set; }
    }
}
