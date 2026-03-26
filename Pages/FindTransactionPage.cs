using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumParaBank.Pages;

public class FindTransactionPage
{
    private readonly IWebDriver _driver;

    public FindTransactionPage(IWebDriver driver)
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
            d.FindElements(By.Id("accountId")).Count > 0 &&
            d.FindElements(By.Id("transactionDate")).Count > 0 &&
            d.FindElements(By.Id("fromDate")).Count > 0 &&
            d.FindElements(By.Id("toDate")).Count > 0 &&
            d.FindElements(By.Id("amount")).Count > 0);
    }

    public void OpenPage()
    {
        var wait = Wait();
        wait.Until(d => d.FindElement(By.LinkText("Find Transactions"))).Click();
        WaitUntilFormReady();
    }

    public bool IsPageDisplayed()
    {
        try
        {
            WaitUntilFormReady();
            return _driver.PageSource.Contains("Find Transactions");
        }
        catch
        {
            return false;
        }
    }

    public bool AreSearchControlsDisplayed()
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

    public void SelectAccount(string account)
    {
        var dropdown = new SelectElement(Wait().Until(d => d.FindElement(By.Id("accountId"))));
        if (!string.IsNullOrWhiteSpace(account))
            dropdown.SelectByText(account);
    }

    public void FindByDate(string date)
    {
        Fill("transactionDate", date);
        ClickFind(0);
        WaitForResult();
    }

    public void FindByDateRange(string fromDate, string toDate)
    {
        Fill("fromDate", fromDate);
        Fill("toDate", toDate);
        ClickFind(1);
        WaitForResult();
    }

    public void FindByAmount(string amount)
    {
        Fill("amount", amount);
        ClickFind(2);
        WaitForResult();
    }

    public bool HasResultTable()
    {
        return _driver.PageSource.Contains("Transaction Results")
            || _driver.FindElements(By.Id("transactionTable")).Any();
    }

    public bool HasNoResultMessage()
    {
        return _driver.PageSource.Contains("No transactions found");
    }

    public bool HasValidationError()
    {
        return _driver.PageSource.Contains("error")
            || _driver.PageSource.Contains("No transactions found");
    }

    private void Fill(string id, string? value)
    {
        var element = Wait().Until(d => d.FindElement(By.Id(id)));
        element.Clear();
        element.SendKeys(value ?? "");
    }

    private void ClickFind(int index)
    {
        var buttons = _driver.FindElements(By.CssSelector("button[type='submit']"));
        if (buttons.Count > index)
        {
            buttons[index].Click();
            return;
        }

        Wait().Until(d => d.FindElement(By.XPath("//button[contains(text(),'Find Transactions')]"))).Click();
    }

    private void WaitForResult()
    {
        Wait().Until(d =>
            d.PageSource.Contains("Transaction Results") ||
            d.PageSource.Contains("No transactions found") ||
            d.PageSource.Contains("error") ||
            d.FindElements(By.Id("transactionTable")).Count > 0);
    }
}