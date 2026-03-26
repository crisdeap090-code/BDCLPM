
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumParaBankTest.Utilities;

public static class DriverFactory
{
    public static IWebDriver InitDriver(string browser = "Chrome")
    {
        var options = new ChromeOptions();
        options.AddArgument("--start-maximized");
        options.AddArgument("--disable-notifications");
        options.AddArgument("--disable-popup-blocking");

        return new ChromeDriver(options);
    }
}
