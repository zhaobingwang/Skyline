using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Skyline.Office.Excel
{
    public class ExcelUtil
    {
        private HSSFWorkbook workbook;
        private FileStream fileStream;

        public void CreateWorkbook(string fileName, string path = null)
        {
            workbook = new HSSFWorkbook();
            if (path == null)
            {
                path = AppDomain.CurrentDomain.BaseDirectory;
            }
            fileStream = new FileStream($"{path}\\{fileName}", FileMode.OpenOrCreate, FileAccess.ReadWrite);
        }

        public void CreateSheet()
        {
            if (workbook == null)
                throw new NullReferenceException($"{nameof(workbook)} is null. Please create a work book first.");
            var sheet = workbook.CreateSheet();
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
