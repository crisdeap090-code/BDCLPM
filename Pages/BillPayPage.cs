
using OpenQA.Selenium;

namespace SeleniumParaBank.Pages;

public class BillPayPage
{
    private readonly IWebDriver _driver;

    public BillPayPage(IWebDriver driver)
    {
        _driver = driver;
    }

    public void OpenBillPay()
    {
        _driver.FindElement(By.LinkText("Bill Pay")).Click();
    }

    public bool IsPageDisplayed()
    {
        return _driver.PageSource.Contains("Bill Payment Service");
    }

    public bool AreAllFieldsDisplayed()
    {
        string[] names =
        {
            "payee.name", "payee.address.street", "payee.address.city", "payee.address.state",
            "payee.address.zipCode", "payee.phoneNumber", "payee.accountNumber", "verifyAccount", "amount"
        };

        return names.All(name => _driver.FindElements(By.Name(name)).Any());
    }

    public void PayBill(
        string name, string address, string city, string state,
        string zip, string phone, string account, string verifyAccount, string amount)
    {
        Fill("payee.name", name);
        Fill("payee.address.street", address);
        Fill("payee.address.city", city);
        Fill("payee.address.state", state);
        Fill("payee.address.zipCode", zip);
        Fill("payee.phoneNumber", phone);
        Fill("payee.accountNumber", account);
        Fill("verifyAccount", verifyAccount);
        Fill("amount", amount);

        _driver.FindElement(By.XPath("//input[@value='Send Payment']")).Click();
    }

    public bool IsPaymentSuccess()
    {
        return _driver.PageSource.Contains("Bill Payment Complete");
    }

    public bool HasValidationError()
    {
        return _driver.PageSource.Contains("is required") || _driver.PageSource.Contains("error");
    }

    public bool HasConfirmation()
    {
        return _driver.PageSource.Contains("Bill Payment Complete");
    }

    private void Fill(string name, string? value)
    {
        var element = _driver.FindElement(By.Name(name));
        element.Clear();
        element.SendKeys(value ?? "");
    }
}
