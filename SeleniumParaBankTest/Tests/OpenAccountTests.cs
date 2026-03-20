using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumParaBank.Pages;
using SeleniumParaBank.Utilities;
using SeleniumParaBankTest.Utilities;
using SeleniumParaBankTest.TestData;
using System.Linq;

namespace SeleniumParaBankTest.Tests
{
    public class OpenAccountTests
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
        public void Run_OpenAccount_Tests_From_Json()
        {
            var data = JsonDataReader.GetOpenAccountData();

            int row = 7;

            foreach (var item in data.Take(10))
            {
                try
                {
                    OpenAccountPage page = new OpenAccountPage(driver);

                    page.OpenPage();

                    if (!string.IsNullOrEmpty(item.accountType))
                        page.SelectAccountType(item.accountType);

                    page.ClickOpenAccount();

                    bool result = page.IsAccountCreated();

                    string actual = result ? "Open Account Success" : "Open Account Fail";

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