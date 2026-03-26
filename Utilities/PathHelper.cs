
namespace SeleniumParaBankTest.Utilities;

public static class PathHelper
{
    public static string ProjectBase => AppDomain.CurrentDomain.BaseDirectory;

    public static string ExcelFile => Path.Combine(ProjectBase, "TestData", "Excel", "Nhom3_AutoMation_Testing.xlsx");
    public static string JsonFolder => Path.Combine(ProjectBase, "TestData", "Json");
    public static string ReportsFolder => Path.Combine(ProjectBase, "Reports");
    public static string ScreenshotsFolderForToday => Path.Combine(ReportsFolder, "Screenshots", DateTime.Now.ToString("yyyy-MM-dd"));

    public static string GetJsonPath(string fileName) => Path.Combine(JsonFolder, fileName);

    public static string EnsureFolder(string folder)
    {
        Directory.CreateDirectory(folder);
        return folder;
    }
}
