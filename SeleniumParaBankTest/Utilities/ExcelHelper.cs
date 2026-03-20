using System;
using System.IO;
using NPOI.XSSF.UserModel;

namespace SeleniumParaBank.Utilities
{
    public class ExcelHelper
    {
        static string file = @"D:\BDCLPM_TH\DOAN\Testcase.xlsx";
        static string sheetName = "TestCase-Chuyển tiền";

        public static void WriteResult(int row, string actual, string status, string screenshot)
        {
            try
            {
                // IN RA ĐƯỜNG DẪN FILE (QUAN TRỌNG NHẤT)
                string fullPath = Path.GetFullPath(file);
                Console.WriteLine("FILE ĐANG GHI: " + fullPath);

                XSSFWorkbook wb;

                // Đọc file
                using (FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                {
                    wb = new XSSFWorkbook(fs);
                }

                var sheet = wb.GetSheet(sheetName);
                if (sheet == null)
                {
                    throw new Exception("❌ Không tìm thấy sheet: " + sheetName);
                }

                var r = sheet.GetRow(row) ?? sheet.CreateRow(row);

                // Ghi dữ liệu vào cột L, M, N
                r.CreateCell(11).SetCellValue(actual);
                r.CreateCell(12).SetCellValue(status);
                r.CreateCell(13).SetCellValue(screenshot);

                // Ghi lại file
                using (FileStream outFile = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                {
                    wb.Write(outFile);
                }

                wb.Close();

                Console.WriteLine($"Ghi thành công dòng {row + 1}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi ghi Excel: " + ex.Message);
                throw;
            }
        }
    }
}