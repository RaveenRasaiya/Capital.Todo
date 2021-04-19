using System;

namespace Capital.Application.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    internal sealed class ValidatedNotNullAttribute : Attribute
    {
    }
}
