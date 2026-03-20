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
        IWebDriver? driver;

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
            int row = 7;

            foreach (var item in data.Take(10))
            {
                try
                {
                    // login trước mỗi test
                    LoginPage login = new LoginPage(driver);
                    login.Login("ThizKi3u", "19032022");

                    LogoutPage page = new LogoutPage(driver);

                    string actual = "";

                    switch (item.action)
                    {
                        case "logout":
                            page.ClickLogout();
                            actual = page.IsLoginPage() ? "logout_success" : "logout_fail";
                            break;

                        case "logout_redirect_home":
                            page.ClickLogout();
                            actual = page.IsCorrectUrl() ? "redirect_home" : "wrong_redirect";
                            break;

                        case "logout_session_remove":
                            page.ClickLogout();
                            driver.Navigate().Refresh();
                            actual = page.IsLoginPage() ? "session_removed" : "session_not_removed";
                            break;

                        case "access_account_overview_after_logout":
                            page.ClickLogout();
                            driver.Navigate().GoToUrl("https://parabank.parasoft.com/parabank/overview.htm");
                            actual = page.IsLoginPage() ? "require_login" : "access_granted";
                            break;

                        case "access_transfer_funds_after_logout":
                            page.ClickLogout();
                            driver.Navigate().GoToUrl("https://parabank.parasoft.com/parabank/transfer.htm");
                            actual = page.IsLoginPage() ? "require_login" : "access_granted";
                            break;

                        case "browser_back_after_logout":
                            page.ClickLogout();
                            driver.Navigate().Back();
                            actual = page.IsLoginPage() ? "access_denied" : "still_accessible";
                            break;

                        case "logout_from_account_overview":
                            page.ClickLogout();
                            actual = page.IsLoginPage() ? "logout_success" : "logout_fail";
                            break;

                        case "logout_from_transfer_funds":
                            page.ClickLogout();
                            actual = page.IsLoginPage() ? "logout_success" : "logout_fail";
                            break;

                        case "check_logout_button":
                            actual = page.IsLogoutButtonDisplayed() ? "button_clickable" : "button_not_found";
                            break;

                        case "repeat_logout":
                            page.ClickLogout();
                            actual = page.IsLoginPage() ? "still_logged_out" : "error";
                            break;

                        default:
                            actual = "unknown_action";
                            break;
                    }

                    string status = (actual == item.expected) ? "PASS" : "FAIL";

                    string screenshot = ScreenshotHelper.Capture(driver);

                    ExcelHelper.WriteResult(row, actual, status, screenshot);
                }
                catch
                {
                    ExcelHelper.WriteResult(row, "ERROR", "FAIL", "");
                }

                row++;
            }
        }

        [TearDown]
        public void TearDown()
        {
            driver?.Quit();
            driver?.Dispose();
        }
    }
}