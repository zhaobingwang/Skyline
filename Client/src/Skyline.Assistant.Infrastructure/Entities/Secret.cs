using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Skyline.Assistant.Infrastructure.Entities
{
    public class Secret
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(32)]
        public string Name { get; set; }

        [Required]
        [StringLength(32)]
        public string Password { get; set; }

        public DateTime CreateTime { get; set; }
        public DateTime ModifyTime { get; set; }
    }
}
