
using OpenQA.Selenium;
using SeleniumParaBankTest.Models;
using SeleniumParaBankTest.Utilities;

namespace SeleniumParaBankTest.Tests;

public abstract class TestBase
{
    protected readonly RunnerSettings Settings;
    protected readonly TestDataResolver Resolver;

    protected TestBase(IWebDriver driver, RunnerSettings settings)
    {
        Settings = settings;
        Resolver = new TestDataResolver(driver, settings);
    }

    protected static bool ExpectPass(string expected) =>
        string.Equals(expected, "Pass", StringComparison.OrdinalIgnoreCase);

    protected static bool ExpectFail(string expected) =>
        string.Equals(expected, "Fail", StringComparison.OrdinalIgnoreCase);

    protected static string PickFirstVariant(string raw) =>
        string.IsNullOrWhiteSpace(raw) ? "" : raw.Split('/')[0].Trim();
}
