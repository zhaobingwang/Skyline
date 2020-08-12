using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.XWPF.UserModel;
using Skyline.Office.Models;
using Skyline.Office.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
        private XWPFDocument document = null;

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

        #region 文本段落
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
            // FIX: AA
            if (paragraphOptions.IsTitle)
            {
                paragraph.Style = paragraphOptions.TitleSize;
            }
        }
        #endregion

        #region 表格段落
        public const int MAX_TABLE_WIDTH = 5000;
        public const int MAX_COLUMN_COUNT = 10;
        public void AddTableParagraph(string title, TableModel model, ParagraphOptions paragraphOptions, TextOptions textOptions)
        {
            if (model == null || model.Rows?.Count < 1)
                throw new ArgumentException(nameof(model));

            bool headerExist = model.Headers != null && model.Headers.Count() > 0;

            var rowCount = model.Rows.Count;
            var columnCount = model.Rows.FirstOrDefault().ColumnValues.Count;
            if (columnCount > MAX_COLUMN_COUNT)
                throw new ArgumentOutOfRangeException("最多只能支持10列");

            if (headerExist)
                rowCount++;

            XWPFTable table = document.CreateTable(rowCount, columnCount);
            table.Width = MAX_TABLE_WIDTH;
            var columnWidth = MAX_TABLE_WIDTH / columnCount;
            for (int i = 0; i < columnCount; i++)
            {
                table.SetColumnWidth(i, (ulong)columnWidth);
            }

            // 表头
            if (headerExist)
            {
                int idxCurrentHeaderColumns = 0;
                foreach (var header in model.Headers)
                {
                    var para = new CT_P();
                    para.AddNewPPr().AddNewTextAlignment(); // 设置单元格文本对齐

                    XWPFParagraph paragraph = new XWPFParagraph(para, table.Body);
                    SetParagraph(paragraph, paragraphOptions);
                    SetText(header, paragraph, textOptions);
                    table.GetRow(0).GetCell(idxCurrentHeaderColumns++).SetParagraph(paragraph);
                }
            }

            // 表格内容
            int idxCurrentRow = 1;
            foreach (var row in model.Rows)
            {
                int idxCurrentHeaderColumns = 0;

                foreach (var columnValue in row.ColumnValues)
                {
                    var para = new CT_P();
                    para.AddNewPPr().AddNewTextAlignment(); // 设置单元格文本对齐

                    XWPFParagraph paragraph = new XWPFParagraph(para, table.Body);
                    SetParagraph(paragraph, paragraphOptions);
                    SetText(columnValue, paragraph, textOptions);
                    table.GetRow(idxCurrentRow).GetCell(idxCurrentHeaderColumns++).SetParagraph(paragraph);
                }
                idxCurrentRow++;
            }

        }
        #endregion
    }
}
