using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Skyline.Assistant.Entities
{
    public class SecretEntity
    {
        public int ID { get; set; }

        [Required]
        [StringLength(32)]
        public string AppName { get; set; }

        [Required]
        [StringLength(32)]
        public string Password { get; set; }

        public DateTime CreateTime { get; set; }
        public DateTime ModifyTime { get; set; }
    }
}
