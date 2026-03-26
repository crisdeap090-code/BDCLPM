
using OpenQA.Selenium;

namespace SeleniumParaBankTest.Utilities;

public static class ScreenshotHelper
{
    public static string Capture(IWebDriver driver, string testCaseId, string status)
    {
        var folder = PathHelper.EnsureFolder(PathHelper.ScreenshotsFolderForToday);
        var fileName = $"{testCaseId}_{status}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
        var fullPath = Path.Combine(folder, fileName);

        var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
        screenshot.SaveAsFile(fullPath);

        return fullPath;
    }
}
