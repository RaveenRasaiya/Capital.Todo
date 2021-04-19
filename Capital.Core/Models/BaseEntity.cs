using System;
using System.ComponentModel.DataAnnotations;

namespace Capital.Core.Models
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
