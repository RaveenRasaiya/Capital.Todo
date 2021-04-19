using Capital.Application.Attributes;
using System;

namespace Capital.Application.Validators
{
    internal static class ArgumentValidator
    {
        public static void NotNull([ValidatedNotNull] object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
        }
    }
}
