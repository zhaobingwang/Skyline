using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Office.Models
{
    public class TableModel
    {
        public string[] Headers { get; set; }
        public List<TableRow> Rows { get; set; }
    }

    public class TableRow
    {
        public string RowValue { get; set; }
        public List<string> ColumnValues { get; set; }
    }
}
