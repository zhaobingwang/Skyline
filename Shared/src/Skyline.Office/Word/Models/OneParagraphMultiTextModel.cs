using Skyline.Office.Word.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Office.Word.Models
{
    public class OneParagraphMultiTextModel
    {
        public string Separator { get; set; }
        public ParagraphOptions ParagraphOptions { get; set; }
        public List<TextModel> TextModels { get; set; }
    }

    public class TextModel
    {
        public string Content { get; set; }
        public TextOptions TextOptions { get; set; }
    }
}
