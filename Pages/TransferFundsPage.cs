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

    private WebDriverWait Wait(int seconds = 10)
    {
        return new WebDriverWait(_driver, TimeSpan.FromSeconds(seconds));
    }

    private void WaitUntilFormReady()
    {
        var wait = Wait();

        wait.Until(d =>
            d.FindElements(By.Id("amount")).Count > 0 &&
            d.FindElements(By.Id("fromAccountId")).Count > 0 &&
            d.FindElements(By.Id("toAccountId")).Count > 0 &&
            d.FindElements(By.CssSelector("input[value='Transfer']")).Count > 0);
    }

    public void OpenPage()
    {
        var wait = Wait();
        wait.Until(d => d.FindElement(By.LinkText("Transfer Funds"))).Click();
        WaitUntilFormReady();
    }

    public bool IsPageDisplayed()
    {
        try
        {
            WaitUntilFormReady();
            return _driver.PageSource.Contains("Transfer Funds");
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

    public void Transfer(string amount, string fromAccount, string toAccount)
    {
        WaitUntilFormReady();
        var wait = Wait();

        var amountInput = wait.Until(d => d.FindElement(By.Id("amount")));
        amountInput.Clear();
        amountInput.SendKeys(amount ?? "");

        var fromDropdown = new SelectElement(wait.Until(d => d.FindElement(By.Id("fromAccountId"))));
        if (!string.IsNullOrWhiteSpace(fromAccount))
            fromDropdown.SelectByText(fromAccount);

        var toDropdown = new SelectElement(wait.Until(d => d.FindElement(By.Id("toAccountId"))));
        if (!string.IsNullOrWhiteSpace(toAccount))
            toDropdown.SelectByText(toAccount);

        wait.Until(d => d.FindElement(By.CssSelector("input[value='Transfer']"))).Click();

        wait.Until(d =>
            d.PageSource.Contains("Transfer Complete!") ||
            d.PageSource.Contains("has been transferred") ||
            d.PageSource.Contains("error") ||
            d.PageSource.Contains("must be"));
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