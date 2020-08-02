using Skyline.Office.Styles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Office.Options
{
    /// <summary>
    /// 段落 选项/设置
    /// </summary>
    public class ParagraphOptions
    {
        /// <summary>
        /// 对齐方式
        /// </summary>
        public Alignment Alignment { get; set; } = Alignment.Left;
    }
}
