using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Capital.Application.Models
{
    public class ToDoModel : IValidatableObject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                yield return new ValidationResult($"Mandatory parameter missing {nameof(Title)}", new[] { nameof(Title) });
            }
            if (StatusId <= 0)
            {
                yield return new ValidationResult($"Invalid value for {nameof(StatusId)}", new[] { nameof(StatusId) });
            }
        }
    }
}
