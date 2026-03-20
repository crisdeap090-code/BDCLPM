using OpenQA.Selenium;

namespace SeleniumParaBank.Pages
{
    public class AccountOverviewPage
    {
        IWebDriver driver;

        public AccountOverviewPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void OpenAccountOverview()
        {
            driver.FindElement(By.LinkText("Accounts Overview")).Click();
        }

        public bool IsAccountTableDisplayed()
        {
            return driver.PageSource.Contains("Account");
        }

        public bool IsBalanceDisplayed()
        {
            return driver.PageSource.Contains("Balance");
        }

        public void ClickFirstAccount()
        {
            driver.FindElement(By.CssSelector("#accountTable a")).Click();
        }

        public bool IsAccountDetailPage()
        {
            return driver.PageSource.Contains("Account Details");
        }

        public bool IsCorrectUrl()
        {
            return driver.Url.Contains("overview.htm");
        }

        public bool IsLoginPage()
        {
            return driver.PageSource.Contains("Customer Login");
        }
    }
}