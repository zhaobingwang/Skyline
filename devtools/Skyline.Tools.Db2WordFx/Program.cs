using Dapper;
using NPOI.XWPF.UserModel;
using Oracle.ManagedDataAccess.Client;
using Skyline.Office;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Skyline.Tools.Db2WordFx
{
    class Program
    {
        static WordUtil wordUtil;
        static void Main(string[] args)
        {
            wordUtil = new WordUtil();
            string connString = ConfigurationManager.ConnectionStrings["oracle"].ToString();

            IDbConnection conncetion = new OracleConnection(connString);
            var tables = GetTables(conncetion);
            var columns = GetColumns(conncetion).ToList();
            var tableTotalCount = tables.Count();

            int idxFile = 1;
            var stream = new FileStream("table.docx", FileMode.Create, FileAccess.ReadWrite);


            XWPFDocument document = new XWPFDocument();
            // Loop all tables.
            int idxCurTable = 1;
            foreach (var table in tables)
            {
                Console.WriteLine($"# {DateTime.Now.ToString("HH:mm:ss fff")} 当前正在处理表: {table.TableName}  处理进度:{idxCurTable}/{tableTotalCount}");
                // Loop all fields of the current table.
                var tableColumns = columns.FindAll(x => x.TableName == table.TableName);//GetTableColumns(conncetion, table.TableName);
                string title = $"{table.TableName}  {table.TableComment}";

                int headerIdx = 0;
                document.SetParagraph(wordUtil.ParagraphSetting(document, title, true, 19, "宋体", ParagraphAlignment.CENTER), headerIdx);
                document.SetParagraph(wordUtil.ParagraphSetting(document, $"ID:{Guid.NewGuid()}", false, 9, "宋体", ParagraphAlignment.LEFT, true, $"    创建时间：{DateTime.Now.ToShortDateString()}"), ++headerIdx);
                headerIdx++;

                XWPFTable firstXwpfTable = document.CreateTable(tableColumns.Count() + 1, 6);
                firstXwpfTable.Width = 6000;
                firstXwpfTable.SetColumnWidth(0, 1000);
                firstXwpfTable.SetColumnWidth(1, 1000);
                firstXwpfTable.SetColumnWidth(2, 1000);
                firstXwpfTable.SetColumnWidth(3, 1000);
                firstXwpfTable.SetColumnWidth(4, 1000);
                firstXwpfTable.SetColumnWidth(5, 1000);

                firstXwpfTable.GetRow(0).GetCell(0).SetParagraph(wordUtil.SetTableParagraphSetting(firstXwpfTable, "序号", ParagraphAlignment.CENTER, 24, true));
                firstXwpfTable.GetRow(0).GetCell(1).SetParagraph(wordUtil.SetTableParagraphSetting(firstXwpfTable, "参数名", ParagraphAlignment.CENTER, 24, true));
                firstXwpfTable.GetRow(0).GetCell(2).SetParagraph(wordUtil.SetTableParagraphSetting(firstXwpfTable, "类型", ParagraphAlignment.CENTER, 24, true));
                firstXwpfTable.GetRow(0).GetCell(3).SetParagraph(wordUtil.SetTableParagraphSetting(firstXwpfTable, "长度", ParagraphAlignment.CENTER, 24, true));
                firstXwpfTable.GetRow(0).GetCell(4).SetParagraph(wordUtil.SetTableParagraphSetting(firstXwpfTable, "可否为空", ParagraphAlignment.CENTER, 24, true));
                firstXwpfTable.GetRow(0).GetCell(5).SetParagraph(wordUtil.SetTableParagraphSetting(firstXwpfTable, "说明", ParagraphAlignment.CENTER, 24, true));

                firstXwpfTable.GetRow(0).GetCell(0).SetColor("#fff312");
                firstXwpfTable.GetRow(0).GetCell(1).SetColor("#fff312");
                firstXwpfTable.GetRow(0).GetCell(2).SetColor("#fff312");
                firstXwpfTable.GetRow(0).GetCell(3).SetColor("#fff312");
                firstXwpfTable.GetRow(0).GetCell(4).SetColor("#fff312");
                firstXwpfTable.GetRow(0).GetCell(5).SetColor("#fff312");

                int currentColumn = 1;
                foreach (var column in tableColumns)
                {
                    firstXwpfTable.GetRow(currentColumn).GetCell(0).SetParagraph(wordUtil.SetTableParagraphSetting(firstXwpfTable, currentColumn.ToString(), ParagraphAlignment.CENTER, 24, true));
                    firstXwpfTable.GetRow(currentColumn).GetCell(1).SetParagraph(wordUtil.SetTableParagraphSetting(firstXwpfTable, column.ColumnName, ParagraphAlignment.CENTER, 24, true));
                    firstXwpfTable.GetRow(currentColumn).GetCell(2).SetParagraph(wordUtil.SetTableParagraphSetting(firstXwpfTable, column.DataType ?? "-", ParagraphAlignment.CENTER, 24, true));
                    firstXwpfTable.GetRow(currentColumn).GetCell(3).SetParagraph(wordUtil.SetTableParagraphSetting(firstXwpfTable, column.DataLength ?? "-", ParagraphAlignment.CENTER, 24, true));
                    firstXwpfTable.GetRow(currentColumn).GetCell(4).SetParagraph(wordUtil.SetTableParagraphSetting(firstXwpfTable, column.Nullable ?? "-", ParagraphAlignment.CENTER, 24, true));
                    firstXwpfTable.GetRow(currentColumn).GetCell(5).SetParagraph(wordUtil.SetTableParagraphSetting(firstXwpfTable, column.ColumnComment ?? "-", ParagraphAlignment.CENTER, 24, true));
                    currentColumn++;
                }
                if (idxCurTable % 100 == 0)
                {
                    //idxFile++;
                    //document.Write(stream);

                    //stream.Close();
                    //stream.Dispose();
                    //document = null;

                    //document = new XWPFDocument();
                    //stream = new FileStream("db" + idxFile + ".docx", FileMode.Create);
                }
                idxCurTable++;
            }

            //if (idxCurTable % 100 != 0)
            document.Write(stream);

            Console.WriteLine("END...");
        }

        private static IEnumerable<TableDto> GetTables(IDbConnection connection)
        {
            string sqlTable = "select a.table_name as TableName,b.comments as TableComment ";
            sqlTable += "from user_tables a ";
            sqlTable += "left join user_tab_comments b on b.table_name = a.table_name ";
            sqlTable += "order by a.table_name";
            return connection.Query<TableDto>(sqlTable);
        }
        [Obsolete]
        private static IEnumerable<TableDetailDto> GetTableColumns(IDbConnection connection, string tableName)
        {
            string sqlTableColumn = "select a.column_name as ColumnName,a.table_name as TableName,a.data_type as DataType,b.comments as ColumnComment ";
            sqlTableColumn += "from user_tab_columns a ";
            sqlTableColumn += "left join user_col_comments b on b.table_name = a.table_name and b.column_name = a.column_name ";
            sqlTableColumn += $"where a.table_name = '{tableName}'";
            return connection.Query<TableDetailDto>(sqlTableColumn);
        }
        private static IEnumerable<TableDetailDto> GetColumns(IDbConnection connection)
        {
            string sqlTableColumn = "select a.column_name as ColumnName,a.table_name as TableName,a.data_type as DataType,a.data_length as DataLength,a.nullable as Nullable,b.comments as ColumnComment ";
            sqlTableColumn += "from user_tab_columns a ";
            sqlTableColumn += "left join user_col_comments b on b.table_name = a.table_name and b.column_name = a.column_name ";
            return connection.Query<TableDetailDto>(sqlTableColumn);
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
        public string DataLength { get; set; }
        public string Nullable { get; set; }
        public string ColumnComment { get; set; }
    }
}
