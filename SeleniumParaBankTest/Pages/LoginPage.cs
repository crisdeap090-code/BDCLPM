using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniumParaBank.Pages
{
    public class LoginPage
    {
        IWebDriver driver;
        WebDriverWait wait;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        IWebElement txtUsername => wait.Until(d => d.FindElement(By.Name("username")));
        IWebElement txtPassword => wait.Until(d => d.FindElement(By.Name("password")));
        IWebElement btnLogin => wait.Until(d => d.FindElement(By.CssSelector("input[value='Log In']")));

        public void Login(string user, string pass)
        {
            txtUsername.Clear();
            txtUsername.SendKeys(user);

            txtPassword.Clear();
            txtPassword.SendKeys(pass);

            btnLogin.Click();
        }

        public bool IsLoginSuccess()
        {
            try
            {
                return driver.PageSource.Contains("Accounts Overview");
            }
            catch
            {
                return false;
            }
        }
    }
}