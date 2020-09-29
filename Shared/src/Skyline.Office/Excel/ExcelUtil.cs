using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using Skyline.Office.Excel.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Skyline.Office.Excel
{
    public class ExcelUtil
    {
        private IWorkbook workbook;
        private FileStream fileStream;
        //private const int MAX_SHEET_COUNT = 30;
        //private List<ISheet> sheetList;
        private ISheet sheet;
        private int ContentRowStart = 0;
        private List<HeaderModel> Header;

        public ExcelUtil()
        {

        }

        public void CreateWorkbook(string fileName, string path = null)
        {
            workbook = new XSSFWorkbook();
            if (path == null)
            {
                path = AppDomain.CurrentDomain.BaseDirectory;
            }
            fileStream = new FileStream($"{path}\\{fileName}", FileMode.OpenOrCreate, FileAccess.ReadWrite);
        }

        public void CreateSheets(string name)
        {
            if (name.IsNull())
                name = new Guid().ToString();
            if (workbook == null)
                throw new NullReferenceException($"{nameof(workbook)} is null. Please create a work book first.");
            sheet = workbook.CreateSheet(name);
        }

        public void AddHeader(string title, List<HeaderModel> hearder, RowOptions options)
        {
            Header = hearder;
            if (!title.IsNullOrWhiteSpace())
            {
                var row = CreateRow(sheet, 0, options.TitleHeight);
                var cell = row.CreateCell(0);

                CellRangeAddress region = new CellRangeAddress(0, 0, 0, hearder.Count() - 1);
                sheet.AddMergedRegion(region);

                cell.SetCellValue(title);

                row = CreateRow(sheet, 1, options.HeaderHeight);
                for (int i = 0; i < hearder.Count; i++)
                {
                    CreateCell(row, i, hearder[i].Name);
                    CreateCell(row, i, hearder[i].Name);
                    if (hearder[i].Width > 0)
                        sheet.SetColumnWidth(i, hearder[i].Width);
                }
                ContentRowStart = 2;
            }
            else
            {
                var row = CreateRow(sheet, 0, options.HeaderHeight);
                for (int i = 0; i < hearder.Count; i++)
                {
                    CreateCell(row, i, hearder[i].Name);
                    CreateCell(row, i, hearder[i].Name);
                    if (hearder[i].Width > 0)
                        sheet.SetColumnWidth(i, hearder[i].Width);
                }
                ContentRowStart = 1;
            }
        }

        public void AddHeader(string title, List<HeaderModel> hearder, RowOptions options, StyleOptions titleStyleOptions, StyleOptions headerStyleOptions)
        {
            Header = hearder;
            if (!title.IsNullOrWhiteSpace())
            {
                var row = CreateRow(sheet, 0, options.TitleHeight);
                var cell = row.CreateCell(0);

                CellRangeAddress region = new CellRangeAddress(0, 0, 0, hearder.Count() - 1);
                sheet.AddMergedRegion(region);

                cell.SetCellValue(title);
                cell.CellStyle = CreateCellStyle(titleStyleOptions);

                row = CreateRow(sheet, 1, options.HeaderHeight);
                for (int i = 0; i < hearder.Count; i++)
                {
                    CreateCell(row, i, hearder[i].Name, headerStyleOptions);
                    if (hearder[i].Width > 0)
                        sheet.SetColumnWidth(i, hearder[i].Width);
                }
                ContentRowStart = 2;
            }
            else
            {
                var row = CreateRow(sheet, 0, options.HeaderHeight);
                for (int i = 0; i < hearder.Count; i++)
                {
                    CreateCell(row, i, hearder[i].Name, headerStyleOptions);
                    if (hearder[i].Width > 0)
                        sheet.SetColumnWidth(i, hearder[i].Width);
                }
                ContentRowStart = 1;
            }
        }

        public void AddContent<T>(List<T> datas)
        {
            if (datas.Count < 1)
                return;
            PropertyInfo[] properties = datas[0].GetType().GetProperties();
            int rowIdx = ContentRowStart;
            foreach (var data in datas)
            {
                var row = sheet.CreateRow(rowIdx);

                for (int i = 0; i < properties.Length; i++)
                {
                    var cellType = properties[i].PropertyType.Name;
                    CreateCell(cellType, row, i, properties[i].GetValue(data));
                }
                rowIdx++;
            }
        }

        public void AddContent<T>(List<T> datas, StyleOptions styleOptions)
        {
            if (datas.Count < 1)
                return;
            PropertyInfo[] properties = datas[0].GetType().GetProperties();
            int rowIdx = ContentRowStart;
            foreach (var data in datas)
            {
                var row = sheet.CreateRow(rowIdx);

                for (int i = 0; i < properties.Length; i++)
                {
                    var cellType = properties[i].PropertyType.Name;
                    CreateCell(cellType, row, i, properties[i].GetValue(data), styleOptions);
                }
                rowIdx++;
            }
        }

        private IRow CreateRow(ISheet sheet, int rowNum, float rowHeight)
        {
            var row = sheet.CreateRow(rowNum);
            row.HeightInPoints = rowHeight;
            return row;
        }


        #region Create Cell
        private void CreateCell(string type, IRow row, int column, object value)
        {
            switch (type)
            {
                case "Boolean":
                    CreateCell(row, column, Convert.ToBoolean(value));
                    break;
                case "Short":
                case "Int16":
                case "Int32":
                case "Int64":
                case "Float":
                case "Double":
                    CreateCell(row, column, Convert.ToDouble(value));
                    break;
                case "Datetime":
                    CreateCell(row, column, Convert.ToDateTime(value));
                    break;
                default:
                    CreateCell(row, column, value.ToString());
                    break;
            }
        }

        private void CreateCell(string type, IRow row, int column, object value, StyleOptions styleOptions)
        {
            switch (type)
            {
                case "Boolean":
                    CreateCell(row, column, Convert.ToBoolean(value), styleOptions);
                    break;
                case "Short":
                case "Int16":
                case "Int32":
                case "Int64":
                case "Float":
                case "Double":
                    CreateCell(row, column, Convert.ToDouble(value), styleOptions);
                    break;
                case "Datetime":
                    CreateCell(row, column, Convert.ToDateTime(value), styleOptions);
                    break;
                default:
                    CreateCell(row, column, value.ToString(), styleOptions);
                    break;
            }
        }

        private ICell CreateCell(IRow row, int column, string cellValue)
        {
            var cell = row.CreateCell(column);
            if (!cellValue.IsNullOrWhiteSpace())
                cell.SetCellValue(cellValue);
            return cell;
        }

        private ICell CreateCell(IRow row, int column, DateTime cellValue)
        {
            var cell = row.CreateCell(column);
            if (cellValue.IsNotNull())
                cell.SetCellValue(cellValue);
            return cell;
        }

        private ICell CreateCell(IRow row, int column, bool cellValue)
        {
            var cell = row.CreateCell(column);
            if (cellValue.IsNotNull())
                cell.SetCellValue(cellValue);
            return cell;
        }

        private ICell CreateCell(IRow row, int column, double cellValue)
        {
            var cell = row.CreateCell(column);
            if (cellValue.IsNotNull())
                cell.SetCellValue(cellValue);
            return cell;
        }

        private ICell CreateCell(IRow row, int column, string cellValue, StyleOptions styleOptions)
        {
            var cell = row.CreateCell(column);
            if (!cellValue.IsNullOrWhiteSpace())
                cell.SetCellValue(cellValue);
            if (styleOptions != null)
                cell.CellStyle = CreateCellStyle(styleOptions);
            return cell;
        }

        private ICell CreateCell(IRow row, int column, DateTime cellValue, StyleOptions styleOptions)
        {
            var cell = row.CreateCell(column);
            if (cellValue.IsNotNull())
                cell.SetCellValue(cellValue);
            if (styleOptions != null)
                cell.CellStyle = CreateCellStyle(styleOptions);
            return cell;
        }

        private ICell CreateCell(IRow row, int column, bool cellValue, StyleOptions styleOptions)
        {
            var cell = row.CreateCell(column);
            if (cellValue.IsNotNull())
                cell.SetCellValue(cellValue);
            if (styleOptions != null)
                cell.CellStyle = CreateCellStyle(styleOptions);
            return cell;
        }

        private ICell CreateCell(IRow row, int column, double cellValue, StyleOptions styleOptions)
        {
            var cell = row.CreateCell(column);
            if (cellValue.IsNotNull())
                cell.SetCellValue(cellValue);
            if (styleOptions != null)
                cell.CellStyle = CreateCellStyle(styleOptions);
            return cell;
        }
        #endregion

        private ICellStyle CreateCellStyle(StyleOptions options)
        {
            // 创建列头单元格实例样式
            ICellStyle cellStyle = workbook.CreateCellStyle();
            if (options.HorizontalAlignment.IsNotNull())
                cellStyle.Alignment = (HorizontalAlignment)options.HorizontalAlignment;
            if (options.VerticalAlignment.IsNotNull())
                cellStyle.VerticalAlignment = (VerticalAlignment)options.VerticalAlignment;

            // 背景色
            if (options.BackgroundColor >= 8)
            {
                cellStyle.FillForegroundColor = options.BackgroundColor;
                cellStyle.FillPattern = FillPattern.SolidForeground;
            }

            var cellFont = workbook.CreateFont();
            cellFont.FontName = options.FontName;
            cellFont.IsBold = options.IsBold;
            cellFont.FontHeightInPoints = options.FontSize;
            cellFont.Color = options.FontColor;

            cellStyle.SetFont(cellFont);

            return cellStyle;
        }

        public void SaveWorkbook()
        {
            workbook.Write(fileStream);
            workbook = null;
            fileStream.Close();
            fileStream.Dispose();
        }

    }
}
