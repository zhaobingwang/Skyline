using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Console.ApplicationCore.VO
{
    public class MenuTreeVO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Field { get; set; }
        public IEnumerable<MenuTreeVO> Children { get; set; }
    }
}
