
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SeleniumParaBankTest.Models;

namespace SeleniumParaBankTest.Utilities;

public class ExcelHelper
{
    private readonly string _filePath;
    private readonly RunnerSettings _settings;

    public ExcelHelper(string filePath, RunnerSettings settings)
    {
        _filePath = filePath;
        _settings = settings;
    }

    public List<TestCaseRow> ReadPendingTestCases()
    {
        using var fs = new FileStream(_filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        IWorkbook workbook = new XSSFWorkbook(fs);

        var wsCases = workbook.GetSheet("TestCases");
        var wsExec = workbook.GetSheet("TestExecution");

        var executionLookup = new Dictionary<string, (int RowIndex, string RunId, string Browser, string Status)>();

        for (int i = 1; i <= wsExec.LastRowNum; i++)
        {
            var row = wsExec.GetRow(i);
            if (row == null) continue;

            string testCaseId = ReadCell(row, 1);
            if (string.IsNullOrWhiteSpace(testCaseId)) continue;

            executionLookup[testCaseId] = (
                i,
                ReadCell(row, 0),
                string.IsNullOrWhiteSpace(ReadCell(row, 9)) ? "Chrome" : ReadCell(row, 9),
                ReadCell(row, 5)
            );
        }

        var result = new List<TestCaseRow>();

        for (int i = 1; i <= wsCases.LastRowNum; i++)
        {
            var row = wsCases.GetRow(i);
            if (row == null) continue;

            string testCaseId = ReadCell(row, 0);
            if (string.IsNullOrWhiteSpace(testCaseId) || !executionLookup.TryGetValue(testCaseId, out var exec)) continue;

            bool shouldRun = !_settings.OnlyRunBlankOrNotRunRows
                || string.IsNullOrWhiteSpace(exec.Status)
                || exec.Status.Equals("Not Run", StringComparison.OrdinalIgnoreCase);

            if (!shouldRun) continue;

            result.Add(new TestCaseRow
            {
                TestCasesRowIndex = i,
                TestExecutionRowIndex = exec.RowIndex,
                RunId = exec.RunId,
                TestCaseId = testCaseId,
                BusinessFlow = ReadCell(row, 1),
                Module = ReadCell(row, 2),
                TestName = ReadCell(row, 3),
                DataSheet = ReadCell(row, 5),
                DataSetId = ReadCell(row, 6),
                Steps = ReadCell(row, 7),
                ExpectedResult = ReadCell(row, 8),
                Priority = ReadCell(row, 9),
                Type = ReadCell(row, 10),
                Browser = exec.Browser
            });
        }

        return result.OrderBy(x => x.TestExecutionRowIndex).ToList();
    }

    public void WriteExecutionResult(TestCaseRow testCase, ExecutionResult result)
    {
        IWorkbook workbook;
        using (var fs = new FileStream(_filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
            workbook = new XSSFWorkbook(fs);
        }

        var wsExec = workbook.GetSheet("TestExecution");
        var row = wsExec.GetRow(testCase.TestExecutionRowIndex) ?? wsExec.CreateRow(testCase.TestExecutionRowIndex);

        SetCell(row, 5, result.Status);
        SetCell(row, 6, result.ActualResult);
        SetCell(row, 7, result.ScreenshotPath);
        SetCell(row, 10, result.Note);

        using var outFs = new FileStream(_filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
        workbook.Write(outFs);
        workbook.Close();
    }

    private static string ReadCell(IRow row, int col)
    {
        var cell = row.GetCell(col);
        if (cell == null) return "";

        return cell.CellType switch
        {
            CellType.Boolean => cell.BooleanCellValue ? "TRUE" : "FALSE",
            CellType.Numeric => DateUtil.IsCellDateFormatted(cell)
                ? (cell.DateCellValue?.ToString("yyyy-MM-dd") ?? "")
                : cell.NumericCellValue.ToString(),
            _ => cell.ToString()?.Trim() ?? ""
        };
    }

    private static void SetCell(IRow row, int col, string value)
    {
        var cell = row.GetCell(col) ?? row.CreateCell(col, CellType.String);
        cell.SetCellValue(value ?? "");
    }
}
