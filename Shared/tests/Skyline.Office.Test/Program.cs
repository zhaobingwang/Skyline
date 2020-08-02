using Skyline.Office.Models;
using Skyline.Office.Options;
using System;
using System.Collections.Generic;
using System.IO;

namespace Skyline.Office.Test
{
    class Program
    {
        static WordUtil wordUtil;
        static void Main(string[] args)
        {
            wordUtil = new WordUtil();
            //CreateTable();
            CustomDemo();
        }

        private static void CustomDemo()
        {
            wordUtil.CreateDocument("sample.docx");
            wordUtil.AddTextParagraph("测试段落1");
            wordUtil.AddTextParagraph(GetLongText());
            wordUtil.AddTextParagraph("测试段落3", new TextOptions { FontStyle = new Styles.FontStyle { ForeColor = "FF00FF" } });

            wordUtil.AddMultiTextParagraph(new List<string> { "多段落1", "多段落2", "多段落3" });
            wordUtil.AddTextParagraphWithMultiText(new List<string> { "单段落多文本段1", "单段落多文本段2", "单段落多文本段3" }, "    |    ");

            wordUtil.AddTextParagraphWithMultiText(GetOneParagraphMultiTextModelFakeData());
            wordUtil.SaveDocument();
        }

        private static OneParagraphMultiTextModel GetOneParagraphMultiTextModelFakeData()
        {
            TextOptions textOption1 = new TextOptions
            {
                FontStyle = new Styles.FontStyle { ForeColor = "FF00FF" }
            };
            TextOptions textOption2 = new TextOptions
            {
                FontStyle = new Styles.FontStyle { ForeColor = "FFD700" }
            };
            TextOptions textOption3 = new TextOptions
            {
                FontStyle = new Styles.FontStyle { ForeColor = "0000CD" }
            };
            OneParagraphMultiTextModel model = new OneParagraphMultiTextModel();
            List<TextModel> textModels = new List<TextModel>();
            textModels.Add(new TextModel { Content = "单段落多文本段多样式1", TextOptions = textOption1 });
            textModels.Add(new TextModel { Content = "单段落多文本段多样式2", TextOptions = textOption2 });
            textModels.Add(new TextModel { Content = "单段落多文本段多样式3", TextOptions = textOption3 });
            model.Separator = "    |    ";
            model.TextModels = textModels;
            return model;
        }

        private static string GetLongText()
        {
            return "NPOI是指构建在POI 3.x版本之上的一个程序，NPOI可以在没有安装Office的情况下对Word或Excel文档进行读写操作。NPOI是一个开源的C#读写Excel、WORD等微软OLE2组件文档的项目。";
        }
    }
}
