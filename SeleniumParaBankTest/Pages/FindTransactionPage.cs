using OpenQA.Selenium;

namespace SeleniumParaBank.Pages
{
    public class FindTransactionPage
    {
        IWebDriver driver;

        public FindTransactionPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void OpenPage()
        {
            driver.FindElement(By.LinkText("Find Transactions")).Click();
        }

        public void SelectAccount(string account)
        {
            driver.FindElement(By.Id("accountId")).SendKeys(account);
        }

        public void EnterTransactionId(string id)
        {
            driver.FindElement(By.Id("transactionId")).SendKeys(id);
        }

        public void EnterDate(string date)
        {
            driver.FindElement(By.Id("transactionDate")).SendKeys(date);
        }

        public void EnterDateRange(string from, string to)
        {
            driver.FindElement(By.Id("fromDate")).SendKeys(from);
            driver.FindElement(By.Id("toDate")).SendKeys(to);
        }

        public void EnterAmount(string amount)
        {
            driver.FindElement(By.Id("amount")).SendKeys(amount);
        }

        public void ClickFind()
        {
            driver.FindElement(By.XPath("//button[contains(text(),'Find Transactions')]")).Click();
        }

        public bool IsResultDisplayed()
        {
            return driver.PageSource.Contains("Transaction Results");
        }
    }
}