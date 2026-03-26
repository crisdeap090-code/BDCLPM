
using OpenQA.Selenium;

namespace SeleniumParaBank.Pages;

public class LogoutPage
{
    private readonly IWebDriver _driver;

    public LogoutPage(IWebDriver driver)
    {
        _driver = driver;
    }

    public void ClickLogout()
    {
        _driver.FindElement(By.LinkText("Log Out")).Click();
    }

    public bool IsLoggedOut()
    {
        return _driver.PageSource.Contains("Customer Login");
    }

    public bool IsLogoutButtonDisplayed()
    {
        return _driver.FindElements(By.LinkText("Log Out")).Any();
    }
}
