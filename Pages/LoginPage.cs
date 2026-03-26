
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumParaBank.Pages;

public class LoginPage
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;

    public LoginPage(IWebDriver driver)
    {
        _driver = driver;
        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
    }

    private IWebElement TxtUsername => _wait.Until(d => d.FindElement(By.Name("username")));
    private IWebElement TxtPassword => _wait.Until(d => d.FindElement(By.Name("password")));
    private IWebElement BtnLogin => _wait.Until(d => d.FindElement(By.CssSelector("input[value='Log In']")));

    public void Login(string username, string password)
    {
        TxtUsername.Clear();
        TxtUsername.SendKeys(username ?? "");
        TxtPassword.Clear();
        TxtPassword.SendKeys(password ?? "");
        BtnLogin.Click();
    }

    public bool AreLoginControlsVisible()
    {
        try
        {
            return TxtUsername.Displayed && TxtPassword.Displayed && BtnLogin.Displayed;
        }
        catch
        {
            return false;
        }
    }

    public bool IsRegisterLinkVisible()
    {
        try
        {
            return _driver.FindElement(By.LinkText("Register")).Displayed;
        }
        catch
        {
            return false;
        }
    }

    public void OpenRegisterPage()
    {
        _wait.Until(d => d.FindElement(By.LinkText("Register"))).Click();
    }

    public bool IsLoginSuccess()
    {
        return _driver.PageSource.Contains("Accounts Overview")
            || _driver.PageSource.Contains("Account Services");
    }

    public bool HasLoginError()
    {
        return _driver.PageSource.Contains("The username and password could not be verified")
            || _driver.PageSource.Contains("Please enter a username and password.");
    }
}
