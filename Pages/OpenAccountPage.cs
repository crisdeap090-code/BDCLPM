using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumParaBank.Pages;

public class OpenAccountPage
{
    private readonly IWebDriver _driver;

    public OpenAccountPage(IWebDriver driver)
    {
        _driver = driver;
    }

    private WebDriverWait Wait(int seconds = 10)
    {
        return new WebDriverWait(_driver, TimeSpan.FromSeconds(seconds));
    }

    private void WaitUntilFormReady()
    {
        var wait = Wait();

        wait.Until(d =>
            d.FindElements(By.Id("type")).Count > 0 &&
            d.FindElements(By.Id("fromAccountId")).Count > 0 &&
            d.FindElements(By.CssSelector("input[value='Open New Account']")).Count > 0);
    }

    public void OpenPage()
    {
        var wait = Wait();
        wait.Until(d => d.FindElement(By.LinkText("Open New Account"))).Click();
        WaitUntilFormReady();
    }

    public bool IsPageDisplayed()
    {
        try
        {
            WaitUntilFormReady();
            return _driver.PageSource.Contains("Open New Account");
        }
        catch
        {
            return false;
        }
    }

    public bool AreControlsDisplayed()
    {
        try
        {
            WaitUntilFormReady();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public void OpenNewAccount(string accountType, string fromAccount)
    {
        WaitUntilFormReady();
        var wait = Wait();

        var typeDropdown = new SelectElement(wait.Until(d => d.FindElement(By.Id("type"))));
        typeDropdown.SelectByText(accountType);

        var fromDropdown = new SelectElement(wait.Until(d => d.FindElement(By.Id("fromAccountId"))));
        if (!string.IsNullOrWhiteSpace(fromAccount))
            fromDropdown.SelectByText(fromAccount);

        wait.Until(d => d.FindElement(By.CssSelector("input[value='Open New Account']"))).Click();

        wait.Until(d =>
            d.PageSource.Contains("Account Opened!") ||
            d.FindElements(By.Id("newAccountId")).Count > 0);
    }

    public bool IsAccountCreated()
    {
        try
        {
            return Wait().Until(d =>
                d.PageSource.Contains("Account Opened!") ||
                d.FindElements(By.Id("newAccountId")).Count > 0);
        }
        catch
        {
            return false;
        }
    }

    public string GetNewAccountId()
    {
        try
        {
            return Wait().Until(d => d.FindElement(By.Id("newAccountId"))).Text.Trim();
        }
        catch
        {
            return "";
        }
    }
}