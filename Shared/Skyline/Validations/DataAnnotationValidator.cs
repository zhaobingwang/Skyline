using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Skyline
{
    /// <summary>
    /// DataAnnotations验证器
    /// </summary>
    public static class DataAnnotationsValidator
    {
        public static bool Validate(object dest, ICollection<ValidationResult> validationResults)
        {
            if (dest == null)
                throw new ArgumentNullException(nameof(dest));

            var context = new ValidationContext(dest, null, null);
            return Validator.TryValidateObject(dest, context, validationResults, true);
        }
    }
}
