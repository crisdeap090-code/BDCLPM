using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumParaBank.Pages;
using SeleniumParaBank.Utilities;
using SeleniumParaBankTest.Utilities;
using SeleniumParaBankTest.TestData;
using System.Linq;

namespace SeleniumParaBankTest.Tests
{
    public class LogoutTests
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = DriverFactory.InitDriver();
            driver.Navigate().GoToUrl("https://parabank.parasoft.com/parabank/index.htm");
        }

        [Test]
        public void Run_Logout_Tests_From_Json()
        {
            var data = JsonDataReader.GetLogoutData();

            int row = 11;

            foreach (var item in data.Take(10)) // chạy 10 test case
            {
                try
                {
                    // luôn login lại trước mỗi test
                    LoginPage login = new LoginPage(driver);
                    login.Login("ThizKi3u", "19032022");

                    LogoutPage page = new LogoutPage(driver);

                    bool result = false;

                    if (item.action == "logout")
                    {
                        page.ClickLogout();
                        result = page.IsLoginPage();
                    }

                    else if (item.action == "logout_redirect_home")
                    {
                        page.ClickLogout();
                        result = page.IsCorrectUrl();
                    }

                    else if (item.action == "logout_session_remove")
                    {
                        page.ClickLogout();
                        driver.Navigate().Refresh();
                        result = page.IsLoginPage();
                    }

                    else if (item.action == "access_account_overview_after_logout")
                    {
                        page.ClickLogout();
                        driver.Navigate().GoToUrl("https://parabank.parasoft.com/parabank/overview.htm");
                        result = page.IsLoginPage();
                    }

                    else if (item.action == "access_transfer_funds_after_logout")
                    {
                        page.ClickLogout();
                        driver.Navigate().GoToUrl("https://parabank.parasoft.com/parabank/transfer.htm");
                        result = page.IsLoginPage();
                    }

                    else if (item.action == "browser_back_after_logout")
                    {
                        page.ClickLogout();
                        driver.Navigate().Back();
                        result = page.IsLoginPage();
                    }

                    else if (item.action == "check_logout_button")
                    {
                        result = page.IsLogoutButtonDisplayed();
                    }

                    else if (item.action == "repeat_logout")
                    {
                        page.ClickLogout();
                        result = page.IsLoginPage();
                    }

                    string actual = result ? "Logout Success" : "Logout Fail";

                    string status = (result == item.expected) ? "PASS" : "FAIL";

                    string screenshot = ScreenshotHelper.Capture(driver);

                    ExcelHelper.WriteResult(row, actual, status, screenshot);
                }
                catch
                {
                    ExcelHelper.WriteResult(row, "ERROR", "FAIL", "");
                }

                row += 10;
            }
        }

        [TearDown]
        public void Close()
        {
            driver.Quit();
        }
    }
}