
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

    public void OpenPage()
    {
        _driver.FindElement(By.LinkText("Open New Account")).Click();
    }

    public bool IsPageDisplayed()
    {
        return _driver.PageSource.Contains("Open New Account");
    }

    public bool AreControlsDisplayed()
    {
        return _driver.FindElements(By.Id("type")).Any()
            && _driver.FindElements(By.Id("fromAccountId")).Any()
            && _driver.FindElements(By.CssSelector("input[value='Open New Account']")).Any();
    }

    public void OpenNewAccount(string accountType, string fromAccount)
    {
        var typeDropdown = new SelectElement(_driver.FindElement(By.Id("type")));
        typeDropdown.SelectByText(accountType);

        var fromDropdown = new SelectElement(_driver.FindElement(By.Id("fromAccountId")));
        if (!string.IsNullOrWhiteSpace(fromAccount))
        {
            fromDropdown.SelectByText(fromAccount);
        }

        _driver.FindElement(By.CssSelector("input[value='Open New Account']")).Click();
    }

    public bool IsAccountCreated()
    {
        return _driver.PageSource.Contains("Account Opened!");
    }

    public string GetNewAccountId()
    {
        try
        {
            return _driver.FindElement(By.Id("newAccountId")).Text.Trim();
        }
        catch
        {
            return "";
        }
    }
}
