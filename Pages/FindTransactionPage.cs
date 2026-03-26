
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

    public void OpenPage()
    {
        _driver.FindElement(By.LinkText("Find Transactions")).Click();
    }

    public bool IsPageDisplayed()
    {
        return _driver.PageSource.Contains("Find Transactions");
    }

    public bool AreSearchControlsDisplayed()
    {
        return _driver.FindElements(By.Id("accountId")).Any()
            && _driver.FindElements(By.Id("transactionDate")).Any()
            && _driver.FindElements(By.Id("fromDate")).Any()
            && _driver.FindElements(By.Id("toDate")).Any()
            && _driver.FindElements(By.Id("amount")).Any();
    }

    public void SelectAccount(string account)
    {
        var dropdown = new SelectElement(_driver.FindElement(By.Id("accountId")));
        if (!string.IsNullOrWhiteSpace(account))
            dropdown.SelectByText(account);
    }

    public void FindByDate(string date)
    {
        Fill("transactionDate", date);
        ClickFind(0);
    }

    public void FindByDateRange(string fromDate, string toDate)
    {
        Fill("fromDate", fromDate);
        Fill("toDate", toDate);
        ClickFind(1);
    }

    public void FindByAmount(string amount)
    {
        Fill("amount", amount);
        ClickFind(2);
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
        var element = _driver.FindElement(By.Id(id));
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

        _driver.FindElement(By.XPath("//button[contains(text(),'Find Transactions')]")).Click();
    }
}
