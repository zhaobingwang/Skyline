using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.XWPF.UserModel;
using Skyline.Office.Models;
using Skyline.Office.Options;
using System;
using System.Collections.Generic;
using System.IO;

namespace Skyline.Office
{
    /// <summary>
    /// Word工具
    /// </summary>
    public class WordUtil
    {
        /// <summary>
        /// 文档对象
        /// </summary>
        private static XWPFDocument document = null;

        /// <summary>
        /// 当前段落行
        /// </summary>
        private int posParagraph;

        /// <summary>
        /// 当前处理文档文件流
        /// </summary>
        FileStream fileStream = null;

        private static readonly object SyncLock = new object();

        /// <summary>
        /// 创建文档对象
        /// </summary>
        /// <param name="fileName">文档名</param>
        /// <param name="path">路径</param>
        public void CreateDocument(string fileName, string path = null)
        {
            posParagraph = 0;
            //if (document == null)
            //{
            //    lock (SyncLock)
            //    {
            //        if (document == null)
            //        {
            //            document = new XWPFDocument();
            //        }
            //    }
            //}
            document = new XWPFDocument();
            if (path == null)
                path = AppDomain.CurrentDomain.BaseDirectory;
            fileStream = new FileStream($"{path}\\{fileName}", FileMode.Create, FileAccess.ReadWrite);
        }

        /// <summary>
        /// 保存文档
        /// </summary>
        public void SaveDocument()
        {
            document.Write(fileStream);
            document = null;
            fileStream.Close();
            fileStream.Dispose();
        }

        /// <summary>
        /// 添加一个文本段落
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public void AddTextParagraph(string content)
        {
            XWPFParagraph paragraph = document.CreateParagraph();   // 创建段落对象
            SetText(content, paragraph);

            document.SetParagraph(paragraph, posParagraph);
            posParagraph++;
        }

        /// <summary>
        /// 添加单文本段 多文本内容 (单一样式)
        /// </summary>
        /// <param name="contents">内容</param>
        /// <param name="separator">分隔符</param>
        public void AddTextParagraphWithMultiText(List<string> contents, string separator = null)
        {
            XWPFParagraph paragraph = document.CreateParagraph();   // 创建段落对象
            int idx = 1;
            foreach (var content in contents)
            {
                SetText(content, paragraph);

                if (idx < contents.Count)
                    SetText(separator, paragraph);

                document.SetParagraph(paragraph, posParagraph);
                idx++;
            }
            posParagraph++;
        }

        /// <summary>
        /// 添加单文本段 多文本内容 (多样式)
        /// </summary>
        /// <param name="model">多文本内容对象模型</param>
        public void AddTextParagraphWithMultiText(OneParagraphMultiTextModel model)
        {
            if (model == null || model.TextModels.Count < 1)
                throw new ArgumentException(nameof(model));
            XWPFParagraph paragraph = document.CreateParagraph();   // 创建段落对象
            int idx = 1;
            foreach (var text in model.TextModels)
            {

                SetText(text.Content, paragraph, text.TextOptions);

                if (idx < model.TextModels.Count)
                    SetText(model.Separator, paragraph);

                document.SetParagraph(paragraph, posParagraph);
                idx++;
            }
            posParagraph++;
        }

        /// <summary>
        /// 添加多文本段
        /// </summary>
        /// <param name="contents">内容</param>
        public void AddMultiTextParagraph(List<string> contents)
        {
            foreach (var content in contents)
            {
                AddTextParagraph(content);
            }
        }

        /// <summary>
        /// 添加一个文本段落
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="paragraphOptions">段落对象属性选项</param>
        public void AddTextParagraph(string content, ParagraphOptions paragraphOptions)
        {
            XWPFParagraph paragraph = document.CreateParagraph();   // 创建段落对象
            SetParagraph(paragraph, paragraphOptions);
            SetText(content, paragraph);
            document.SetParagraph(paragraph, posParagraph);
            posParagraph++;
        }

        /// <summary>
        /// 添加一个文本段落
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="textOptions">文本对象属性选项</param>
        public void AddTextParagraph(string content, TextOptions textOptions)
        {
            XWPFParagraph paragraph = document.CreateParagraph();   // 创建段落对象

            SetText(content, paragraph, textOptions);
            document.SetParagraph(paragraph, posParagraph);
            posParagraph++;
        }

        /// <summary>
        /// 添加一个文本段落
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="paragraphOptions">段落对象属性选项</param>
        /// <param name="textOptions">文本对象属性选项</param>
        public void AddTextParagraph(string content, ParagraphOptions paragraphOptions, TextOptions textOptions)
        {
            XWPFParagraph paragraph = document.CreateParagraph();   // 创建段落对象
            SetParagraph(paragraph, paragraphOptions);
            SetText(content, paragraph, textOptions);
            document.SetParagraph(paragraph, posParagraph);
            posParagraph++;
        }

        /// <summary>
        /// 文本对象设置
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="paragraph">段落对象</param>
        /// <param name="textOptions">文本对象属性选项</param>
        private void SetText(string content, XWPFParagraph paragraph, TextOptions textOptions)
        {
            XWPFRun txt = paragraph.CreateRun();    // 创建文本对象
            txt.SetText(content);

            if (textOptions == null)
                return;

            txt.IsBold = textOptions.FontStyle.IsBold;
            txt.IsBold = textOptions.FontStyle.IsBold;
            txt.FontSize = textOptions.FontStyle.FontSize;
            txt.SetColor(textOptions.FontStyle.ForeColor);
        }

        /// <summary>
        /// 文本对象设置
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="paragraph">段落对象</param>
        private void SetText(string content, XWPFParagraph paragraph)
        {
            XWPFRun txt = paragraph.CreateRun();    // 创建文本对象
            txt.SetText(content);
        }

        /// <summary>
        /// 段落设置
        /// </summary>
        /// <param name="paragraph">段落对象</param>
        /// <param name="paragraphOptions">段落对象属性选项</param>
        private void SetParagraph(XWPFParagraph paragraph, ParagraphOptions paragraphOptions)
        {
            if (paragraphOptions == null)
                return;
            paragraph.Alignment = (ParagraphAlignment)paragraphOptions.Alignment;
        }

        public void AddTableParagraph(string title)
        {
            var para = new CT_P();
            para.AddNewPPr().AddNewTextAlignment();
        }


        /// <summary>
        /// 创建word文档中的段落对象和设置段落文本的基本样式（字体大小，字体，字体颜色，字体对齐位置）
        /// </summary>
        /// <param name="document">document文档对象</param>
        /// <param name="fillContent">段落第一个文本对象填充的内容</param>
        /// <param name="isBold">是否加粗</param>
        /// <param name="fontSize">字体大小</param>
        /// <param name="fontFamily">字体</param>
        /// <param name="paragraphAlign">段落排列（左对齐，居中，右对齐）</param>
        /// <param name="isStatement">是否在同一段落创建第二个文本对象（解决同一段落里面需要填充两个或者多个文本值的情况，多个文本需要自己拓展，现在最多支持两个）</param>
        /// <param name="secondFillContent">第二次声明的文本对象填充的内容，样式与第一次的一致</param>
        /// <param name="fontColor">字体颜色--十六进制</param>
        /// <param name="isItalic">是否设置斜体（字体倾斜）</param>
        /// <returns></returns>
        public XWPFParagraph ParagraphSetting(XWPFDocument document, string fillContent, bool isBold, int fontSize, string fontFamily, ParagraphAlignment paragraphAlign, bool isStatement = false, string secondFillContent = "", string fontColor = "000000", bool isItalic = false)
        {
            XWPFParagraph paragraph = document.CreateParagraph();//创建段落对象
            paragraph.Alignment = paragraphAlign;//文字显示位置,段落排列（左对齐，居中，右对齐）


            XWPFRun xwpfRun = paragraph.CreateRun();//创建段落文本对象
            xwpfRun.IsBold = isBold;//文字加粗
            xwpfRun.SetText(fillContent);//填充内容
            xwpfRun.FontSize = fontSize;//设置文字大小
            xwpfRun.IsItalic = isItalic;//是否设置斜体（字体倾斜）
            xwpfRun.SetColor(fontColor);//设置字体颜色--十六进制

            xwpfRun.SetFontFamily(fontFamily, FontCharRange.None); //设置标题样式如：（微软雅黑，隶书，楷体）根据自己的需求而定

            if (!isStatement) return paragraph;

            XWPFRun secondXwpfRun = paragraph.CreateRun();//创建段落文本对象
            secondXwpfRun.IsBold = isBold;//文字加粗
            secondXwpfRun.SetText(secondFillContent);//填充内容
            secondXwpfRun.FontSize = fontSize;//设置文字大小
            secondXwpfRun.IsItalic = isItalic;//是否设置斜体（字体倾斜）
            secondXwpfRun.SetColor(fontColor);//设置字体颜色--十六进制
            secondXwpfRun.SetFontFamily(fontFamily, FontCharRange.None); //设置标题样式如：（微软雅黑，隶书，楷体）根据自己的需求而定

            return paragraph;
        }


        /// <summary> 
        /// 创建Word文档中表格段落实例和设置表格段落文本的基本样式（字体大小，字体，字体颜色，字体对齐位置）
        /// </summary> 
        /// <param name="table">表格对象</param> 
        /// <param name="fillContent">要填充的文字</param> 
        /// <param name="paragraphAlign">段落排列（左对齐，居中，右对齐）</param>
        /// <param name="textPosition">设置文本位置（设置两行之间的行间,从而实现表格文字垂直居中的效果），从而实现table的高度设置效果 </param>
        /// <param name="isBold">是否加粗（true加粗，false不加粗）</param>
        /// <param name="fontSize">字体大小</param>
        /// <param name="fontColor">字体颜色--十六进制</param>
        /// <param name="isItalic">是否设置斜体（字体倾斜）</param>
        /// <returns></returns> 
        public XWPFParagraph SetTableParagraphSetting(XWPFTable table, string fillContent, ParagraphAlignment paragraphAlign, int textPosition = 24, bool isBold = false, int fontSize = 10, string fontColor = "000000", bool isItalic = false)
        {
            var para = new CT_P();
            //设置单元格文本对齐
            para.AddNewPPr().AddNewTextAlignment();

            XWPFParagraph paragraph = new XWPFParagraph(para, table.Body);//创建表格中的段落对象
            paragraph.Alignment = paragraphAlign;//文字显示位置,段落排列（左对齐，居中，右对齐）
                                                 //paragraph.FontAlignment =Convert.ToInt32(ParagraphAlignment.CENTER);//字体在单元格内显示位置与 paragraph.Alignment效果相似

            XWPFRun xwpfRun = paragraph.CreateRun();//创建段落文本对象
            xwpfRun.SetText(fillContent);
            xwpfRun.FontSize = fontSize;//字体大小
            xwpfRun.SetColor(fontColor);//设置字体颜色--十六进制
            xwpfRun.IsItalic = isItalic;//是否设置斜体（字体倾斜）
            xwpfRun.IsBold = isBold;//是否加粗
            xwpfRun.SetFontFamily("宋体", FontCharRange.None);//设置字体（如：微软雅黑,华文楷体,宋体）
            xwpfRun.TextPosition = textPosition;//设置文本位置（设置两行之间的行间），从而实现table的高度设置效果 
            return paragraph;
        }
    }
}
