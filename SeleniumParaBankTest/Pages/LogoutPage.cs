using OpenQA.Selenium;

namespace SeleniumParaBank.Pages
{
    public class LogoutPage
    {
        IWebDriver driver;

        public LogoutPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        // Click nút Logout
        public void ClickLogout()
        {
            driver.FindElement(By.LinkText("Log Out")).Click();
        }

        // Kiểm tra đã quay về trang Login
        public bool IsLoginPage()
        {
            return driver.PageSource.Contains("Customer Login");
        }

        // Kiểm tra form login hiển thị
        public bool IsLoginFormDisplayed()
        {
            try
            {
                return driver.FindElement(By.Name("username")).Displayed;
            }
            catch
            {
                return false;
            }
        }

        // Kiểm tra URL sau khi logout
        public bool IsCorrectUrl()
        {
            return driver.Url.Contains("index.htm");
        }

        // Kiểm tra nút Logout tồn tại
        public bool IsLogoutButtonDisplayed()
        {
            try
            {
                return driver.FindElement(By.LinkText("Log Out")).Displayed;
            }
            catch
            {
                return false;
            }
        }
    }
}