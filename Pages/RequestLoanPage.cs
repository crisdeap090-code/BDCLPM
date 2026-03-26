
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

    public void OpenPage()
    {
        _wait.Until(d => d.FindElement(By.LinkText("Request Loan"))).Click();
    }

    public bool IsPageDisplayed()
    {
        return _driver.PageSource.Contains("Apply for a Loan");
    }

    public void RequestLoan(string loanAmount, string downPayment, string fromAccount)
    {
        Fill("amount", loanAmount);
        Fill("downPayment", downPayment);

        var dropdown = new SelectElement(_driver.FindElement(By.Id("fromAccountId")));
        if (!string.IsNullOrWhiteSpace(fromAccount))
            dropdown.SelectByText(fromAccount);

        _driver.FindElement(By.CssSelector("input[value='Apply Now']")).Click();
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
        var element = _driver.FindElement(By.Id(id));
        element.Clear();
        element.SendKeys(value ?? "");
    }
}
