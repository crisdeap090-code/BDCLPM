
using OpenQA.Selenium;

namespace SeleniumParaBank.Pages;

public class UpdateProfilePage
{
    private readonly IWebDriver _driver;

    public UpdateProfilePage(IWebDriver driver)
    {
        _driver = driver;
    }

    public void OpenPage()
    {
        _driver.FindElement(By.LinkText("Update Contact Info")).Click();
    }

    public bool IsPageDisplayed()
    {
        return _driver.PageSource.Contains("Update Profile");
    }

    public bool HasPrefilledCurrentData()
    {
        return !string.IsNullOrWhiteSpace(GetValue("customer.firstName"))
            && !string.IsNullOrWhiteSpace(GetValue("customer.lastName"));
    }

    public void FillForm(string firstName, string lastName, string address, string city, string state, string zip, string phone)
    {
        Fill("customer.firstName", firstName);
        Fill("customer.lastName", lastName);
        Fill("customer.address.street", address);
        Fill("customer.address.city", city);
        Fill("customer.address.state", state);
        Fill("customer.address.zipCode", zip);
        Fill("customer.phoneNumber", phone);
    }

    public void ClickUpdate()
    {
        _driver.FindElement(By.CssSelector("input[value='Update Profile']")).Click();
    }

    public bool IsUpdateSuccess()
    {
        return _driver.PageSource.Contains("Profile Updated");
    }

    public string GetValue(string id)
    {
        return _driver.FindElement(By.Id(id)).GetAttribute("value") ?? "";
    }

    private void Fill(string id, string? value)
    {
        var element = _driver.FindElement(By.Id(id));
        element.Clear();
        element.SendKeys(value ?? "");
    }
}
