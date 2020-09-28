using NPOI.SS.UserModel;
using Skyline.Office.Excel.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Skyline.Office.Excel.Options
{
    // TODO:Color将short改为color与NPOI解耦
    public class StyleOptions
    {
        public SkylineHorizontalAlignment HorizontalAlignment { get; set; }
        public SkylineVerticalAlignment VerticalAlignment { get; set; }
        //public Color BackgroundColor { get; set; }
        public short BackgroundColor { get; set; }

        public string FontName { get; set; } = "宋体";
        public short FontSize { get; set; } = 11;
        //public Color FontColor { get; set; }
        public short FontColor { get; set; }
        public bool IsBold { get; set; } = false;
    }
}
