using NPOI.XWPF.UserModel;
using Skyline.Office.Word;
using Skyline.Office.Word.Models;
using Skyline.Office.Word.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Skyline.Office.Test
{
    class Program
    {
        static WordUtil wordUtil;
        static void Main(string[] args)
        {
            wordUtil = new WordUtil();
            WriteByReadTemplate();
            //CreateTable();
            CustomDemo();
        }
        public static bool ContainsUnicodeCharacter(string input)
        {
            const int MaxAnsiCode = 255;

            return input.Any(c => c > MaxAnsiCode);
        }

        private static void WriteByReadTemplate()
        {
            using (var dotStream = new FileStream("read.docx", FileMode.Open, FileAccess.Read))
            {
                XWPFDocument template = new XWPFDocument(dotStream);

                using (var fileStream = new FileStream("test.docx", FileMode.Create, FileAccess.Write))
                {
                    XWPFDocument document = new XWPFDocument();
                    XWPFStyles newStyles = document.CreateStyles();
                    newStyles.SetStyles(template.GetCTStyle());

                    XWPFParagraph paragraph = document.CreateParagraph();
                    paragraph.Style = "a3";
                    XWPFRun xwpfRun = paragraph.CreateRun();
                    xwpfRun.SetText("标题内容");

                    XWPFParagraph paragraph1 = document.CreateParagraph();
                    paragraph1.Style = "1";
                    XWPFRun xwpfRun1 = paragraph1.CreateRun();
                    xwpfRun1.SetText("标题1内容");

                    XWPFParagraph paragraph2 = document.CreateParagraph();
                    paragraph2.Style = "2";
                    XWPFRun xwpfRun2 = paragraph2.CreateRun();
                    xwpfRun2.SetText("标题2内容");

                    document.Write(fileStream);

                    document.Close();

                }
                template.Close();
            }
        }

        private static void CustomDemo()
        {
            // TODO: 部分“空”Unicode字符写入word导致word打不开，WPS可以
            #region  暂不处理
            //var aa = "\u4f60\u597d";
            //var bb = "\u0013";
            //var cc = ContainsUnicodeCharacter("\u0013");
            //var ccc = ContainsUnicodeCharacter("好h");
            //var dd = ContainsUnicodeCharacter("\u4f60\u597d"); 
            #endregion

            wordUtil.CreateDocument("sample.docx");

            wordUtil.AddTextParagraph("标题1", new ParagraphOptions { IsTitle = true, TitleSize = "1" });

            wordUtil.AddTextParagraph("标题2", new ParagraphOptions { IsTitle = true, TitleSize = "2" });

            wordUtil.AddTextParagraph("标题3", new ParagraphOptions { IsTitle = true, TitleSize = "标题 1" });

            wordUtil.AddTextParagraph("测试\u4f60\u597d段落1\u263A\u0021\u1F60F");
            wordUtil.AddTextParagraph(GetLongText());
            wordUtil.AddTextParagraph("测试段落3", new TextOptions { FontStyle = new Word.Styles.FontStyle { ForeColor = "FF00FF" } });

            wordUtil.AddMultiTextParagraph(new List<string> { "多段落1", "多段落2", "多段落3" });
            wordUtil.AddTextParagraphWithMultiText(new List<string> { "单段落多文本段1", "单段落多文本段2", "单段落多文本段3" }, "    |    ");

            wordUtil.AddTextParagraphWithMultiText(GetOneParagraphMultiTextModelFakeData());
            wordUtil.AddTextParagraphWithMultiText(GetOneParagraphMultiTextModelFakeData());

            // 表格
            wordUtil.AddTableParagraph("table name", GetTableModel(5, 10), new ParagraphOptions { Alignment = Word.Styles.Alignment.Center }, new TextOptions { FontStyle = new Word.Styles.FontStyle { IsBold = true } });

            wordUtil.AddTextParagraph("--------------------------");

            wordUtil.AddTableParagraph("table name", GetTableModel(5, 10), new ParagraphOptions { Alignment = Word.Styles.Alignment.Center }, new TextOptions { FontStyle = new Word.Styles.FontStyle { IsBold = true } });

            wordUtil.AddTextParagraph("--------------------------");

            wordUtil.SaveDocument();
        }

        private static OneParagraphMultiTextModel GetOneParagraphMultiTextModelFakeData()
        {
            TextOptions textOption1 = new TextOptions
            {
                FontStyle = new Word.Styles.FontStyle { ForeColor = "FF00FF" }
            };
            TextOptions textOption2 = new TextOptions
            {
                FontStyle = new Word.Styles.FontStyle { ForeColor = "FFD700" }
            };
            TextOptions textOption3 = new TextOptions
            {
                FontStyle = new Word.Styles.FontStyle { ForeColor = "0000CD" }
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

        private static TableModel GetTableModel(int rowCount, int columnCount)
        {
            TableModel tableModel = new TableModel();
            var headers = new string[columnCount];
            for (int i = 1; i <= columnCount; i++)
            {
                headers[i - 1] = $"表头{i}";
            }
            tableModel.Headers = headers;
            tableModel.Rows = new List<TableRow>();

            for (int i = 1; i <= rowCount; i++)
            {
                TableRow tableRowColumn = new TableRow();
                tableRowColumn.RowValue = $"第{i}行";

                var columnValues = new List<string>();
                for (int j = 1; j < columnCount + 1; j++)
                {
                    columnValues.Add($"{i}-{j}");
                }
                tableRowColumn.ColumnValues = columnValues;
                tableModel.Rows.Add(tableRowColumn);
            }
            return tableModel;
        }
    }
}
