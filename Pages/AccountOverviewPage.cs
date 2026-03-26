
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumParaBank.Pages;

public class AccountOverviewPage
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;

    public AccountOverviewPage(IWebDriver driver)
    {
        _driver = driver;
        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
    }

    public void OpenAccountOverview()
    {
        _wait.Until(d => d.FindElement(By.LinkText("Accounts Overview"))).Click();
    }

    public bool IsAccountServicesMenuDisplayed()
    {
        return _driver.PageSource.Contains("Account Services");
    }

    public bool IsPageDisplayed()
    {
        return _driver.Url.Contains("overview.htm") || _driver.PageSource.Contains("Accounts Overview");
    }

    public bool IsAccountTableDisplayed()
    {
        return _driver.FindElements(By.Id("accountTable")).Any();
    }

    public bool HasBalanceInformation()
    {
        return _driver.FindElements(By.XPath("//table[@id='accountTable']//td[contains(text(),'$')]")).Any();
    }

    public List<string> GetAccountNumbers()
    {
        return _driver.FindElements(By.CssSelector("#accountTable tbody tr td:first-child a"))
            .Select(x => x.Text.Trim())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToList();
    }

    public void ClickFirstAccount()
    {
        _wait.Until(d => d.FindElement(By.CssSelector("#accountTable tbody tr td:first-child a"))).Click();
    }

    public bool IsAccountDetailDisplayed()
    {
        return _driver.PageSource.Contains("Account Details");
    }

    public bool IsTransactionTableDisplayed()
    {
        return _driver.FindElements(By.Id("transactionTable")).Any()
            || _driver.PageSource.Contains("Transactions");
    }

    public bool HasTransactionHeaders()
    {
        var source = _driver.PageSource;
        return source.Contains("Date") && source.Contains("Transaction") && (source.Contains("Debit") || source.Contains("Amount"));
    }

    public bool CanAccessInternalPageAfterLogout()
    {
        _driver.Navigate().GoToUrl("https://parabank.parasoft.com/parabank/overview.htm");
        return _driver.PageSource.Contains("Accounts Overview");
    }
}
