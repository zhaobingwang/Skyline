using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Skyline.Utilities.UnitTests.Validations
{
    public class DataAnnotationsSampleModel
    {
        public int Int32Value { get; set; }

        [Required]

        [MaxLength(4, ErrorMessage = "长度不能超过4")]
        [MinLength(2, ErrorMessage = "长度不能小于2")]
        public string StringValue { get; set; }
    }
}
