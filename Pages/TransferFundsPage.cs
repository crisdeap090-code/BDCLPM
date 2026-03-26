
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumParaBank.Pages;

public class TransferFundsPage
{
    private readonly IWebDriver _driver;

    public TransferFundsPage(IWebDriver driver)
    {
        _driver = driver;
    }

    public void OpenPage()
    {
        _driver.FindElement(By.LinkText("Transfer Funds")).Click();
    }

    public bool IsPageDisplayed()
    {
        return _driver.PageSource.Contains("Transfer Funds");
    }

    public bool AreControlsDisplayed()
    {
        return _driver.FindElements(By.Id("amount")).Any()
            && _driver.FindElements(By.Id("fromAccountId")).Any()
            && _driver.FindElements(By.Id("toAccountId")).Any();
    }

    public void Transfer(string amount, string fromAccount, string toAccount)
    {
        var amountInput = _driver.FindElement(By.Id("amount"));
        amountInput.Clear();
        amountInput.SendKeys(amount ?? "");

        var fromDropdown = new SelectElement(_driver.FindElement(By.Id("fromAccountId")));
        if (!string.IsNullOrWhiteSpace(fromAccount))
            fromDropdown.SelectByText(fromAccount);

        var toDropdown = new SelectElement(_driver.FindElement(By.Id("toAccountId")));
        if (!string.IsNullOrWhiteSpace(toAccount))
            toDropdown.SelectByText(toAccount);

        _driver.FindElement(By.CssSelector("input[value='Transfer']")).Click();
    }

    public bool IsTransferSuccess()
    {
        return _driver.PageSource.Contains("Transfer Complete!");
    }

    public bool HasValidationError()
    {
        return _driver.PageSource.Contains("error") || _driver.PageSource.Contains("must be");
    }

    public bool HasConfirmation()
    {
        return _driver.PageSource.Contains("has been transferred");
    }
}
