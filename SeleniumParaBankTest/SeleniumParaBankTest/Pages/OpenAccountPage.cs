using OpenQA.Selenium;

namespace SeleniumParaBank.Pages
{
    public class OpenAccountPage
    {
        IWebDriver driver;

        public OpenAccountPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void OpenPage()
        {
            driver.FindElement(By.LinkText("Open New Account")).Click();
        }

        public void SelectAccountType(string type)
        {
            driver.FindElement(By.Id("type")).SendKeys(type);
        }

        public void ClickOpenAccount()
        {
            driver.FindElement(By.XPath("//input[@value='Open New Account']")).Click();
        }

        public bool IsAccountCreated()
        {
            return driver.PageSource.Contains("Account Opened!");
        }

        public bool IsPageDisplayed()
        {
            return driver.PageSource.Contains("Open New Account");
        }

        public bool IsCorrectUrl()
        {
            return driver.Url.Contains("openaccount.htm");
        }
    }
}