using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumParaBank.Pages;
using SeleniumParaBank.Utilities;
using SeleniumParaBankTest.Utilities;
using SeleniumParaBankTest.TestData;
using System.Linq;

namespace SeleniumParaBankTest.Tests
{
    public class AccountOverviewTests
    {
        IWebDriver? driver;

        [SetUp]
        public void Setup()
        {
            driver = DriverFactory.InitDriver();

            driver.Navigate().GoToUrl("https://parabank.parasoft.com/parabank/index.htm");

            LoginPage login = new LoginPage(driver);
            login.Login("ThizKi3u", "19032022");
        }

        [Test]
        public void Run_AccountOverview_Tests_From_Json()
        {
            var data = JsonDataReader.GetAccountOverviewData();

            int row = 7;

            foreach (var item in data.Take(10))
            {
                try
                {
                    AccountOverviewPage page = new AccountOverviewPage(driver);

                    bool result = false;

                    switch (item.action)
                    {
                        case "open_account_overview":
                            page.OpenAccountOverview();
                            result = page.IsAccountTableDisplayed();
                            break;

                        case "check_balance_display":
                            page.OpenAccountOverview();
                            result = page.IsBalanceDisplayed();
                            break;

                        case "open_account_detail":
                            page.OpenAccountOverview();
                            page.ClickFirstAccount();
                            result = page.IsAccountDetailPage();
                            break;

                        case "check_account_type":
                            page.OpenAccountOverview();
                            result = page.IsAccountTableDisplayed();
                            break;

                        case "check_account_overview_url":
                            page.OpenAccountOverview();
                            result = page.IsCorrectUrl();
                            break;

                        case "check_account_table_data":
                            page.OpenAccountOverview();
                            result = page.IsAccountTableDisplayed();
                            break;

                        case "access_without_login":
                            driver.Navigate().GoToUrl("https://parabank.parasoft.com/parabank/overview.htm");
                            result = page.IsLoginPage();
                            break;

                        case "check_page_load":
                            page.OpenAccountOverview();
                            result = page.IsAccountTableDisplayed();
                            break;

                        case "check_table_layout":
                            page.OpenAccountOverview();
                            result = page.IsAccountTableDisplayed();
                            break;

                        case "refresh_account_overview":
                            driver.Navigate().Refresh();
                            result = page.IsAccountTableDisplayed();
                            break;
                    }

                    string actual = result ? "Account Overview Success" : "Account Overview Fail";

                    string status = (result == item.expected) ? "PASS" : "FAIL";

                    string screenshot = ScreenshotHelper.Capture(driver);

                    ExcelHelper.WriteResult(row, actual, status, screenshot);
                }
                catch
                {
                    ExcelHelper.WriteResult(row, "ERROR", "FAIL", "");
                }

                row ++;
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