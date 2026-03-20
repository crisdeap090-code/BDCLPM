using OpenQA.Selenium;

namespace SeleniumParaBank.Pages
{
    public class TransferFundsPage
    {
        IWebDriver driver;

        public TransferFundsPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void OpenPage()
        {
            driver.FindElement(By.LinkText("Transfer Funds")).Click();
        }

        public void EnterAmount(string amount)
        {
            driver.FindElement(By.Id("amount")).Clear();
            driver.FindElement(By.Id("amount")).SendKeys(amount);
        }

        public void SelectFromAccount(string account)
        {
            driver.FindElement(By.Id("fromAccountId")).SendKeys(account);
        }

        public void SelectToAccount(string account)
        {
            driver.FindElement(By.Id("toAccountId")).SendKeys(account);
        }

        public void ClickTransfer()
        {
            driver.FindElement(By.XPath("//input[@value='Transfer']")).Click();
        }

        public bool IsTransferSuccess()
        {
            return driver.PageSource.Contains("Transfer Complete");
        }
    }
}