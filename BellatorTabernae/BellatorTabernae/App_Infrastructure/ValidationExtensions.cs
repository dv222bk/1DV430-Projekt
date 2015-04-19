
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BellatorTabernae
{
    public static class ValidationExtensions
    {
        public static bool Validate<T>(this T instance, out ICollection<ValidationResult> validationResults)
        {
            var validationContext = new ValidationContext(instance);
            validationResults = new List<ValidationResult>();
            return Validator.TryValidateObject(instance, validationContext, validationResults, true);
        }
    }
}