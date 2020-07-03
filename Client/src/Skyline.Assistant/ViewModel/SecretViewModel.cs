using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Assistant.UI.WPF.ViewModel
{
    public class SecretViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string CreateTime { get; set; }
        public string ModifyTime { get; set; }
    }
}
