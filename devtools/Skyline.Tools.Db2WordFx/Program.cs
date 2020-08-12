using Dapper;
using NPOI.XWPF.UserModel;
using Oracle.ManagedDataAccess.Client;
using Skyline.Office;
using Skyline.Office.Models;
using Skyline.Office.Options;
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
            wordUtil.CreateDocument("table.docx");
            string connString = ConfigurationManager.ConnectionStrings["oracle"].ToString();

            IDbConnection conncetion = new OracleConnection(connString);
            var tables = GetTables(conncetion);
            var columns = GetColumns(conncetion).ToList();
            var tableTotalCount = tables.Count();

            XWPFDocument document = new XWPFDocument();
            // Loop all tables.
            int idxCurTable = 1;
            foreach (var table in tables)
            {
                Console.WriteLine($"# {DateTime.Now.ToString("HH:mm:ss fff")} 当前正在处理表: {table.TableName}  处理进度:{idxCurTable}/{tableTotalCount}");
                // Loop all fields of the current table.
                var tableColumns = columns.FindAll(x => x.TableName == table.TableName);
                string title = $"{table.TableName}  {table.TableComment}";

                wordUtil.AddTextParagraph(title, new ParagraphOptions { Alignment = Office.Styles.Alignment.Center }, new TextOptions { FontStyle = new Office.Styles.FontStyle { FontSize = 18, IsBold = true } });
                wordUtil.AddTextParagraph($"ID: {Guid.NewGuid()}    创建时间：{DateTime.Now.ToShortDateString()}");

                TableModel tableModel = new TableModel();
                tableModel.Headers = new string[] { "序号", "参数名", "类型", "长度", "可否为空", "说明" };
                tableModel.Rows = new List<TableRow>();
                int idxRow = 1;
                foreach (var column in tableColumns)
                {
                    TableRow tableRowColumn = new TableRow();
                    tableRowColumn.RowValue = $"第{idxRow}行";
                    var columnValues = new List<string>();
                    columnValues.Add(idxRow.ToString());
                    columnValues.Add(column.ColumnName);
                    columnValues.Add(column.DataType);
                    columnValues.Add(column.DataLength);
                    columnValues.Add(column.Nullable);
                    columnValues.Add(column.ColumnComment ?? "-");

                    tableRowColumn.ColumnValues = columnValues;
                    tableModel.Rows.Add(tableRowColumn);
                    idxRow++;

                }
                wordUtil.AddTableParagraph(table.TableName, tableModel, new ParagraphOptions { Alignment = Office.Styles.Alignment.Center }, new TextOptions { FontStyle = new Office.Styles.FontStyle { } });

                idxCurTable++;
            }
            wordUtil.SaveDocument();
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
