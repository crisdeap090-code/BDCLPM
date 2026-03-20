using OpenQA.Selenium;

namespace SeleniumParaBank.Pages
{
    public class UpdateProfilePage
    {
        IWebDriver driver;

        public UpdateProfilePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void OpenPage()
        {
            driver.FindElement(By.LinkText("Update Contact Info")).Click();
        }

        public void FillForm(
            string firstName,
            string lastName,
            string address,
            string city,
            string state,
            string zip,
            string phone
        )
        {
            var f = driver.FindElement(By.Id("customer.firstName"));
            f.Clear();
            f.SendKeys(firstName);

            var l = driver.FindElement(By.Id("customer.lastName"));
            l.Clear();
            l.SendKeys(lastName);

            var a = driver.FindElement(By.Id("customer.address.street"));
            a.Clear();
            a.SendKeys(address);

            var c = driver.FindElement(By.Id("customer.address.city"));
            c.Clear();
            c.SendKeys(city);

            var s = driver.FindElement(By.Id("customer.address.state"));
            s.Clear();
            s.SendKeys(state);

            var z = driver.FindElement(By.Id("customer.address.zipCode"));
            z.Clear();
            z.SendKeys(zip);

            var p = driver.FindElement(By.Id("customer.phoneNumber"));
            p.Clear();
            p.SendKeys(phone);
        }

        public void ClickUpdate()
        {
            driver.FindElement(By.XPath("//input[@value='Update Profile']")).Click();
        }

        public bool IsUpdateSuccess()
        {
            return driver.PageSource.Contains("Profile Updated");
        }
    }
}