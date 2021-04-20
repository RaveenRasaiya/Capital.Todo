using Capital.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Capital.Core.Entities
{
    public class ToDoItem : BaseEntity
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public ToDoItemStatus Status { get; set; }
    }
}
