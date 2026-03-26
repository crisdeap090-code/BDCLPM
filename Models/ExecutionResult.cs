
namespace SeleniumParaBankTest.Models;

public class ExecutionResult
{
    public string Status { get; set; } = "Fail";
    public string ActualResult { get; set; } = "";
    public string ScreenshotPath { get; set; } = "";
    public string Note { get; set; } = "";

    public static ExecutionResult Pass(string actual) => new()
    {
        Status = "Pass",
        ActualResult = actual
    };

    public static ExecutionResult Fail(string actual, string? note = null) => new()
    {
        Status = "Fail",
        ActualResult = actual,
        Note = note ?? ""
    };

    public static ExecutionResult Blocked(string actual, string? note = null) => new()
    {
        Status = "Blocked",
        ActualResult = actual,
        Note = note ?? ""
    };
}
