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

        /// <summary>
        /// 颜色值，参考NPOI对应的Excel56种颜色
        /// </summary>
        public short BackgroundColor { get; set; } = -1;

        public string FontName { get; set; } = "宋体";

        public short FontSize { get; set; } = 11;

        /// <summary>
        /// 颜色值，参考NPOI对应的Excel56种颜色
        /// </summary>
        public short FontColor { get; set; } = 8;

        public bool IsBold { get; set; } = false;
    }
}
