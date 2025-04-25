using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ShiftBackend.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RequiredIfTrueAttribute : ValidationAttribute
    {
        private readonly string _dependentProperty;

        public RequiredIfTrueAttribute(string dependentProperty)
        {
            _dependentProperty = dependentProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo property = validationContext.ObjectType.GetProperty(_dependentProperty);

            if (property == null)
                return new ValidationResult($"Unknown property: {_dependentProperty}");

            if (property.PropertyType != typeof(bool) && property.PropertyType != typeof(bool?))
                return new ValidationResult($"Property '{_dependentProperty}' must be of type bool or nullable bool.");

            bool isTrue = (bool?)property.GetValue(validationContext.ObjectInstance) == true;

            if (isTrue && (value == null || string.IsNullOrWhiteSpace(value.ToString())))
            {
                string errorMessage = ErrorMessage ?? $"{validationContext.DisplayName} is required when {_dependentProperty} is true.";
                return new ValidationResult(errorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
