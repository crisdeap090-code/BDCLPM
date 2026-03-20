using OpenQA.Selenium;

namespace SeleniumParaBank.Utilities
{
    public class ScreenshotHelper
    {
        public static string Capture(IWebDriver driver)
        {
            string folder = Path.Combine(Directory.GetCurrentDirectory(), "Screenshots");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            string file = "shot_" + DateTime.Now.Ticks + ".png";

            string fullPath = Path.Combine(folder, file);

            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
            ss.SaveAsFile(fullPath);

            return "file:///" + fullPath.Replace("\\", "/");
        }
    }
}