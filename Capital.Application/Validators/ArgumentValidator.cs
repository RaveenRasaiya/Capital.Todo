using Capital.Application.Exceptions;
using Capital.Core.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Capital.Application.Validators
{
    public class ArgumentValidator : IArgumentValidator
    {
        public void Validate(dynamic input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(input);
            }

            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(input, new ValidationContext(input), validationResults, true))
            {
                throw new ApiException(validationResults[0].ErrorMessage);
            }
        }
    }
}
