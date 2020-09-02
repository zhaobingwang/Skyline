using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skyline.Console.WebMvc.Models
{
    public class MenuVO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public IEnumerable<MenuVO> Children { get; set; }
    }
}
