using OpenQA.Selenium;

namespace SeleniumParaBank.Pages
{
    public class RequestLoanPage
    {
        IWebDriver driver;

        public RequestLoanPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        // mở trang Request Loan
        public void OpenPage()
        {
            driver.FindElement(By.LinkText("Request Loan")).Click();
        }

        // nhập Loan Amount
        public void EnterLoanAmount(string amount)
        {
            var box = driver.FindElement(By.Id("amount"));
            box.Clear();
            box.SendKeys(amount);
        }

        // nhập Down Payment
        public void EnterDownPayment(string payment)
        {
            var box = driver.FindElement(By.Id("downPayment"));
            box.Clear();
            box.SendKeys(payment);
        }

        // click Apply Now
        public void ClickApply()
        {
            driver.FindElement(By.XPath("//input[@value='Apply Now']")).Click();
        }

        // kiểm tra loan thành công
        public bool IsLoanApproved()
        {
            return driver.PageSource.Contains("Loan Request Processed");
        }
    }
}