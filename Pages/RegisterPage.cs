
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumParaBank.Pages;

public class RegisterPage
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;

    public RegisterPage(IWebDriver driver)
    {
        _driver = driver;
        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
    }

    public void OpenFromHome()
    {
        _wait.Until(d => d.FindElement(By.LinkText("Register"))).Click();
    }

    public bool IsPageDisplayed()
    {
        return _driver.Url.Contains("register.htm") || _driver.PageSource.Contains("Signing up is easy!");
    }

    public bool AreAllFieldsDisplayed()
    {
        string[] ids =
        {
            "customer.firstName", "customer.lastName", "customer.address.street",
            "customer.address.city", "customer.address.state", "customer.address.zipCode",
            "customer.phoneNumber", "customer.ssn", "customer.username",
            "customer.password", "repeatedPassword"
        };

        return ids.All(id => _driver.FindElements(By.Id(id)).Any());
    }

    public void Register(
        string firstName, string lastName, string address, string city, string state, string zip,
        string phone, string ssn, string username, string password, string confirm)
    {
        Input("customer.firstName", firstName);
        Input("customer.lastName", lastName);
        Input("customer.address.street", address);
        Input("customer.address.city", city);
        Input("customer.address.state", state);
        Input("customer.address.zipCode", zip);
        Input("customer.phoneNumber", phone);
        Input("customer.ssn", ssn);
        Input("customer.username", username);
        Input("customer.password", password);
        Input("repeatedPassword", confirm);

        _driver.FindElement(By.CssSelector("input[value='Register']")).Click();
    }

    public bool IsRegisterSuccess()
    {
        return _driver.PageSource.Contains("Your account was created successfully");
    }

    public bool HasRequiredValidationError()
    {
        return _driver.PageSource.Contains("is required");
    }

    public bool HasPasswordMismatchError()
    {
        return _driver.PageSource.Contains("Passwords did not match");
    }

    public bool HasDuplicateUsernameError()
    {
        return _driver.PageSource.Contains("This username already exists.");
    }

    private void Input(string id, string? value)
    {
        var el = _wait.Until(d => d.FindElement(By.Id(id)));
        el.Clear();
        el.SendKeys(value ?? "");
    }
}
