using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumParaBank.Utilities
{
    public class DriverFactory
    {
        public static IWebDriver InitDriver()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            return driver;
        }
    }
}