using OpenQA.Selenium;

namespace SeleniumParaBank.Pages
{
    public class BillPayPage
    {
        IWebDriver driver;

        public BillPayPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void OpenBillPay()
        {
            driver.FindElement(By.LinkText("Bill Pay")).Click();
        }

        public void EnterPayeeName(string name)
        {
            driver.FindElement(By.Name("payee.name")).SendKeys(name);
        }

        public void EnterAddress(string address)
        {
            driver.FindElement(By.Name("payee.address.street")).SendKeys(address);
        }

        public void EnterCity(string city)
        {
            driver.FindElement(By.Name("payee.address.city")).SendKeys(city);
        }

        public void EnterState(string state)
        {
            driver.FindElement(By.Name("payee.address.state")).SendKeys(state);
        }

        public void EnterZip(string zip)
        {
            driver.FindElement(By.Name("payee.address.zipCode")).SendKeys(zip);
        }

        public void EnterPhone(string phone)
        {
            driver.FindElement(By.Name("payee.phoneNumber")).SendKeys(phone);
        }

        public void EnterAccount(string acc)
        {
            driver.FindElement(By.Name("payee.accountNumber")).SendKeys(acc);
        }

        public void EnterVerifyAccount(string acc)
        {
            driver.FindElement(By.Name("verifyAccount")).SendKeys(acc);
        }

        public void EnterAmount(string amount)
        {
            driver.FindElement(By.Name("amount")).SendKeys(amount);
        }

        public void ClickSendPayment()
        {
            driver.FindElement(By.XPath("//input[@value='Send Payment']")).Click();
        }

        public bool IsPaymentSuccess()
        {
            return driver.PageSource.Contains("Bill Payment Complete");
        }
    }
}