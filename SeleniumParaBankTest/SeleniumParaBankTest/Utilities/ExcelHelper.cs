using System;
using System.IO;
using NPOI.XSSF.UserModel;

namespace SeleniumParaBank.Utilities
{
    public class ExcelHelper
    {
        static string file = @"D:\BDCLPM_TH\DOAN\Testcase.xlsx";
        static string sheetName = "TestCase-Đăng ký";

        public static void WriteResult(int row, string actual, string status, string screenshot)
        {
            XSSFWorkbook wb;

            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                wb = new XSSFWorkbook(fs);
            }

            var sheet = wb.GetSheet(sheetName);

            var r = sheet.GetRow(row);
            if (r == null)
            {
                r = sheet.CreateRow(row);
            }

            if (r.GetCell(11) == null) r.CreateCell(11);
            if (r.GetCell(12) == null) r.CreateCell(12);
            if (r.GetCell(13) == null) r.CreateCell(13);

            r.GetCell(11).SetCellValue(actual);
            r.GetCell(12).SetCellValue(status);
            r.GetCell(13).SetCellValue(screenshot);

            using (FileStream outFile = new FileStream(file, FileMode.Create, FileAccess.Write))
            {
                wb.Write(outFile);
            }
        }
    }
}