
namespace SeleniumParaBankTest.Models;

public class TestCaseRow
{
    public int TestCasesRowIndex { get; set; }
    public int TestExecutionRowIndex { get; set; }
    public string RunId { get; set; } = "";
    public string TestCaseId { get; set; } = "";
    public string BusinessFlow { get; set; } = "";
    public string Module { get; set; } = "";
    public string TestName { get; set; } = "";
    public string DataSheet { get; set; } = "";
    public string DataSetId { get; set; } = "";
    public string Steps { get; set; } = "";
    public string ExpectedResult { get; set; } = "";
    public string Priority { get; set; } = "";
    public string Type { get; set; } = "";
    public string Browser { get; set; } = "Chrome";

    public bool HasData => !string.IsNullOrWhiteSpace(DataSheet) && !string.IsNullOrWhiteSpace(DataSetId);
}
