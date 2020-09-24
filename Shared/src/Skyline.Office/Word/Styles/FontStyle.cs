using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Office.Word.Styles
{
    public class FontStyle
    {
        public int FontSize { get; set; } = 11;
        public bool IsBold { get; set; } = false;
        public bool IsItalic { get; set; } = false;
        public string ForeColor { get; set; } = "000000";
    }
}
