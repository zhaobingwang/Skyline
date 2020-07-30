using NPOI.XWPF.UserModel;
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
            CreateTable();
        }

        private static void CreateTable()
        {
            List<TableDto> tables = new List<TableDto>() {
                new TableDto{ TableName="tb1",TableComment="tbaaa"},
                new TableDto{ TableName="tb2",TableComment="tbbbb"}
            };

            List<TableDetailDto> details = new List<TableDetailDto>() {
                new TableDetailDto{ TableName="tb1",DataType="varchar",ColumnName="c1",ColumnComment="comment1"},
                new TableDetailDto{ TableName="tb1",DataType="varchar",ColumnName="c2",ColumnComment="comment2"},
                new TableDetailDto{ TableName="tb2",DataType="varchar",ColumnName="c3",ColumnComment="comment1"},
                new TableDetailDto{ TableName="tb2",DataType="varchar",ColumnName="c4",ColumnComment="comment2"}
            };

            string workFileName = "数据结构";
            using (var stream = new FileStream("sample.docx", FileMode.Create, FileAccess.ReadWrite))
            {
                // 创建document文档对象对象实例
                XWPFDocument document = new XWPFDocument();

                int headerIdx = 0;
                foreach (var table in tables)
                {
                    workFileName = table.TableName;
                    var code = $"文档编号{headerIdx}:" + Guid.NewGuid();
                    document.SetParagraph(wordUtil.ParagraphSetting(document, workFileName, true, 19, "宋体", ParagraphAlignment.CENTER), headerIdx);
                    document.SetParagraph(wordUtil.ParagraphSetting(document, code, false, 9, "宋体", ParagraphAlignment.LEFT, true, $"    创建时间：{DateTime.Now.ToShortDateString()}"), ++headerIdx);
                    headerIdx++;
                    List<TableDetailDto> detailsDto = details.FindAll(c => c.TableName == table.TableName);

                    // 创建文档中的表格对象实例
                    XWPFTable firstXwpfTable = document.CreateTable(detailsDto.Count + 1, 3);
                    firstXwpfTable.Width = 5200;//总宽度
                    firstXwpfTable.SetColumnWidth(0, 1300); /* 设置列宽 */
                    firstXwpfTable.SetColumnWidth(1, 1100); /* 设置列宽 */
                    firstXwpfTable.SetColumnWidth(2, 1400); /* 设置列宽 */

                    firstXwpfTable.GetRow(0).GetCell(0).SetParagraph(wordUtil.SetTableParagraphSetting(firstXwpfTable, "列名", ParagraphAlignment.CENTER, 24, true));
                    firstXwpfTable.GetRow(0).GetCell(1).SetParagraph(wordUtil.SetTableParagraphSetting(firstXwpfTable, "类型", ParagraphAlignment.CENTER, 24, true));
                    firstXwpfTable.GetRow(0).GetCell(2).SetParagraph(wordUtil.SetTableParagraphSetting(firstXwpfTable, "注释", ParagraphAlignment.CENTER, 24, true));

                    firstXwpfTable.GetRow(0).GetCell(0).SetColor("#fff312");
                    firstXwpfTable.GetRow(0).GetCell(1).SetColor("#fff312");
                    firstXwpfTable.GetRow(0).GetCell(2).SetColor("#fff312");

                    int i = 1;
                    foreach (var item in detailsDto)
                    {
                        //Table 表格第一行展示...后面的都是一样，只改变GetRow中的行数
                        firstXwpfTable.GetRow(i).GetCell(0).SetParagraph(wordUtil.SetTableParagraphSetting(firstXwpfTable, item.ColumnName, ParagraphAlignment.CENTER, 24, true));
                        firstXwpfTable.GetRow(i).GetCell(1).SetParagraph(wordUtil.SetTableParagraphSetting(firstXwpfTable, item.DataType, ParagraphAlignment.CENTER, 24, true));
                        firstXwpfTable.GetRow(i).GetCell(2).SetParagraph(wordUtil.SetTableParagraphSetting(firstXwpfTable, item.ColumnComment, ParagraphAlignment.CENTER, 24, true));
                        i++;
                    }
                }

                document.Write(stream);
            }
        }

        public class TableDto
        {
            public string TableName { get; set; }
            public string TableComment { get; set; }
        }

        public class TableDetailDto
        {
            public string TableName { get; set; }
            public string ColumnName { get; set; }
            public string DataType { get; set; }
            public string ColumnComment { get; set; }
        }
    }
}
