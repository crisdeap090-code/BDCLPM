using OpenQA.Selenium;

namespace SeleniumParaBank.Pages
{
    public class RegisterPage
    {
        IWebDriver driver;

        public RegisterPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void OpenPage()
        {
            driver.FindElement(By.LinkText("Register")).Click();
        }

        public void FillForm(string first, string last, string address,
                             string city, string state, string zip,
                             string phone, string ssn,
                             string user, string pass, string confirm)
        {
            driver.FindElement(By.Id("customer.firstName")).SendKeys(first);
            driver.FindElement(By.Id("customer.lastName")).SendKeys(last);
            driver.FindElement(By.Id("customer.address.street")).SendKeys(address);
            driver.FindElement(By.Id("customer.address.city")).SendKeys(city);
            driver.FindElement(By.Id("customer.address.state")).SendKeys(state);
            driver.FindElement(By.Id("customer.address.zipCode")).SendKeys(zip);
            driver.FindElement(By.Id("customer.phoneNumber")).SendKeys(phone);
            driver.FindElement(By.Id("customer.ssn")).SendKeys(ssn);

            driver.FindElement(By.Id("customer.username")).SendKeys(user);
            driver.FindElement(By.Id("customer.password")).SendKeys(pass);
            driver.FindElement(By.Id("repeatedPassword")).SendKeys(confirm);
        }

        public void ClickRegister()
        {
            driver.FindElement(By.XPath("//input[@value='Register']")).Click();
        }

        public bool IsRegisterSuccess()
        {
            return driver.PageSource.Contains("Your account was created successfully");
        }
    }
}