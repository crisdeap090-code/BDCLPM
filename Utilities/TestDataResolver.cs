
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumParaBank.Pages;
using SeleniumParaBankTest.Models;

namespace SeleniumParaBankTest.Utilities;

public class TestDataResolver
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;
    private readonly RunnerSettings _settings;

    public TestDataResolver(IWebDriver driver, RunnerSettings settings)
    {
        _driver = driver;
        _settings = settings;
        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
    }

    public void GoHome()
    {
        _driver.Navigate().GoToUrl(_settings.BaseUrl);
    }

    public void LoginAsExistingUser()
    {
        GoHome();
        var loginPage = new LoginPage(_driver);
        loginPage.Login(_settings.ExistingUsername, _settings.ExistingPassword);
        _wait.Until(d => d.PageSource.Contains("Accounts Overview") || d.PageSource.Contains("Account Services"));
    }

    public void LogoutIfPossible()
    {
        try
        {
            if (_driver.FindElements(By.LinkText("Log Out")).Count > 0)
            {
                _driver.FindElement(By.LinkText("Log Out")).Click();
                _wait.Until(d => d.PageSource.Contains("Customer Login"));
            }
        }
        catch
        {
        }
        finally
        {
            try
            {
                _driver.Manage().Cookies.DeleteAllCookies();
            }
            catch
            {
            }
        }
    }

    public string ResolveAccountAlias(string alias)
    {
        if (string.IsNullOrWhiteSpace(alias)) return "";
        if (!alias.StartsWith("ACCOUNT_", StringComparison.OrdinalIgnoreCase)) return alias;

        var page = new AccountOverviewPage(_driver);
        page.OpenAccountOverview();
        var accounts = page.GetAccountNumbers();

        if (accounts.Count == 0) return "";

        if (alias.Equals("ACCOUNT_A", StringComparison.OrdinalIgnoreCase))
            return accounts.ElementAtOrDefault(0) ?? "";

        if (alias.Equals("ACCOUNT_B", StringComparison.OrdinalIgnoreCase))
            return accounts.ElementAtOrDefault(1) ?? accounts.ElementAtOrDefault(0) ?? "";

        return accounts.ElementAtOrDefault(0) ?? "";
    }

    public string NormalizeAmount(string raw)
    {
        if (string.IsNullOrWhiteSpace(raw)) return "";
        return raw.Split('/')[0].Trim();
    }

    public string BuildUniqueUsername(string baseUsername)
    {
        if (string.IsNullOrWhiteSpace(baseUsername)) return "";
        return $"{baseUsername}_{DateTime.Now:HHmmss}";
    }
}
