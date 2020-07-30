using Dapper;
using Oracle.ManagedDataAccess.Client;
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
        static void Main(string[] args)
        {
            string connString = ConfigurationManager.ConnectionStrings["oracle"].ToString();

            IDbConnection conncetion = new OracleConnection(connString);
            var tables = GetTables(conncetion);

            // Loop all tables.
            foreach (var table in tables)
            {
                // Loop all fields of the current table.
                var tableColumns = GetTableColumns(conncetion, table.TableName);
                foreach (var column in tableColumns)
                {
                }
            }
        }

        private static IEnumerable<TableDto> GetTables(IDbConnection connection)
        {
            string sqlTable = "select a.table_name as TableName,b.comments as TableComment ";
            sqlTable += "from user_tables a ";
            sqlTable += "left join user_tab_comments b on b.table_name = a.table_name ";
            sqlTable += "order by a.table_name";
            return connection.Query<TableDto>(sqlTable);
        }
        private static IEnumerable<TableDetailDto> GetTableColumns(IDbConnection connection, string tableName)
        {
            string sqlTableColumn = "select a.column_name as ColumnName,a.table_name as TableName,a.data_type as DataType,b.comments as ColumnComment ";
            sqlTableColumn += "from user_tab_columns a ";
            sqlTableColumn += "left join user_col_comments b on b.table_name = a.table_name and b.column_name = a.column_name ";
            sqlTableColumn += $"where a.table_name = '{tableName}'";
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
        public string ColumnComment { get; set; }
    }
}
