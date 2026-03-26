using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumParaBank.Pages;

public class RequestLoanPage
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;

    public RequestLoanPage(IWebDriver driver)
    {
        _driver = driver;
        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
    }

    private void WaitUntilFormReady()
    {
        _wait.Until(d =>
            d.FindElements(By.Id("amount")).Count > 0 &&
            d.FindElements(By.Id("downPayment")).Count > 0 &&
            d.FindElements(By.Id("fromAccountId")).Count > 0 &&
            d.FindElements(By.CssSelector("input[value='Apply Now']")).Count > 0);
    }

    public void OpenPage()
    {
        _wait.Until(d => d.FindElement(By.LinkText("Request Loan"))).Click();
        WaitUntilFormReady();
    }

    public bool IsPageDisplayed()
    {
        try
        {
            WaitUntilFormReady();
            return _driver.PageSource.Contains("Apply for a Loan");
        }
        catch
        {
            return false;
        }
    }

    public void RequestLoan(string loanAmount, string downPayment, string fromAccount)
    {
        WaitUntilFormReady();

        Fill("amount", loanAmount);
        Fill("downPayment", downPayment);

        var dropdown = new SelectElement(_wait.Until(d => d.FindElement(By.Id("fromAccountId"))));
        if (!string.IsNullOrWhiteSpace(fromAccount))
            dropdown.SelectByText(fromAccount);

        _wait.Until(d => d.FindElement(By.CssSelector("input[value='Apply Now']"))).Click();

        _wait.Until(d =>
            d.PageSource.Contains("Loan Request Processed") ||
            d.PageSource.Contains("Denied") ||
            d.PageSource.Contains("error"));
    }

    public bool IsLoanProcessed()
    {
        return _driver.PageSource.Contains("Loan Request Processed");
    }

    public bool IsLoanApproved()
    {
        return _driver.PageSource.Contains("Approved");
    }

    public bool HasValidationError()
    {
        return _driver.PageSource.Contains("error") || _driver.PageSource.Contains("Denied");
    }

    private void Fill(string id, string? value)
    {
        var element = _wait.Until(d => d.FindElement(By.Id(id)));
        element.Clear();
        element.SendKeys(value ?? "");
    }
}